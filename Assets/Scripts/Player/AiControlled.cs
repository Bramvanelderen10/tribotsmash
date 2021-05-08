using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tribot;

/// <summary>
/// Handles the AI
/// </summary>
public class AiControlled : TribotBehaviour
{
    enum AiState
    {
        Regular,
        Targeting,
    }

    AiState _state = AiState.Regular;

    //Settings
    private AiData _data;

    private Player _player;
    private PlayerAbilityHandler _abilityHandler;
    private PlayerStateMachine _stateMachine;
    private Rigidbody _rb;

    //Player always rotates towards this euler angle
    private Vector3 _targetEulerAngle = Vector3.zero;
    private GameObject _targetObject = null;

    //All the lists of objects the AI needs to know
    private List<GameObject> _players;
    private List<GameObject> _pickups;
    private List<GameObject> _controlPoints;
    private List<GameObject> _projectiles;
    private List<GameObject> _meteors;

    private bool _refreshed = true;
    private float _timestamp = 0f;
    private float _rangedAttackTimestamp = 0f;
    private bool OnTarget = false;

    Quaternion _angle
    {
        get
        {
            var angle = Vector3.zero;
            switch (_state)
            {
                case AiState.Regular:

                    angle = _targetEulerAngle;
                    break;
                case AiState.Targeting:
                    if (!_targetObject)
                        angle = _targetEulerAngle;
                    else
                        angle = transform.LookRotation(
                            _targetObject.transform.position.x,
                            transform.position.y,
                            _targetObject.transform.position.z).eulerAngles;
                    break;
                default:
                    angle = _targetEulerAngle;
                    break;
            }

            return Quaternion.Euler(0f, angle.y, 0f);
        }
    }

    Vector3 _newPosition
    {
        get
        {
            var speed = _data.MoveSpeed;
            switch (_state)
            {
                case AiState.Regular:
                    //DONT DO SHIT
                    break;
                case AiState.Targeting:
                    speed = _data.MoveSpeed * .3f;
                    break;
            }
            _stateMachine.Speed = speed / _data.MoveSpeed;

            return transform.position + (transform.forward * speed * Time.deltaTime);
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _player = GetComponent<Player>();
        _abilityHandler = GetComponent<PlayerAbilityHandler>();

        _data = Resources.Load<AiData>("AiData"); //Retrieve AI settings from scriptedobject
        _players = TargetContainer.Instance.Players;
        _pickups = TargetContainer.Instance.Pickups;
        _controlPoints = TargetContainer.Instance.ControlPoints;
        _projectiles = TargetContainer.Instance.Projectiles;
        _meteors = TargetContainer.Instance.Meteors;
    }

    void Update()
    {
        

    }

    void FixedUpdate()
    {
        if (Time.time < _timestamp)
        {
            _refreshed = false;
        }
        else
        {
            _refreshed = true;
            _timestamp = Time.time + _data.RefreshRate.Random;
        }

        OnTarget = false;
        var obj = GetClosestTargetFromList(_controlPoints);
        if (obj && GetDistance(obj) < 1.5f)
            OnTarget = true;

        if (_stateMachine.CanInteract)
        {
            DetermineAttack();
            if (_refreshed)
            {
                if (!DetermineIfStuck())
                {
                    DetermineRotation();
                }
            }
        }


        if (_stateMachine.CanMove)
        {
            //Rotate and move
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle, Time.deltaTime * _data.RotationSpeed);
            if (!OnTarget)
                _rb.MovePosition(_newPosition);
        }
    }

    void DetermineRotation()
    {
        Quaternion rotation;

        var meteor = GetClosestTargetFromList(_meteors);
        if (meteor && GetDistance(meteor) < 2.2f)
        {
            Log("Run away from meteor");
            var dir = (transform.position - meteor.transform.PositionXZ(transform.position)).normalized;
            rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            var obj = GetOptimalTarget();
            if (obj == null)
                return;
            Log("new target: " + obj.name);
            rotation = transform.LookRotation(obj.transform.position);
        }

        
        var euler = rotation.eulerAngles;
        _targetEulerAngle = euler;
    }

    bool DetermineIfStuck()
    {
        var obj = CheckFrontSide(_data.CloseRangeDistance);
        if (obj != null)
        {
            Log("Stuck, looking for a way out");
            _targetEulerAngle = GetOptimalRotation();
            return true;
        }

        return false;
    }

    void DetermineAttack()
    {
        //Cache these to prevent huge GC cycles
        Ability dash = null;
        Ability shield = null;
        Ability punch = null;
        Ability grab = null;
        Ability shockwave = null;
        Ability shockblast = null;
        Ability wall = null;
        Ability rangedgrab = null;
        Ability bomb = null;

        foreach (var ability in _player.Abilities)
        {
            if (ability.Value.IsReady)
            {
                if (ability.Value.GetType() == typeof (AbilityDash))
                {
                    dash = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityShield))
                {
                    shield = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityPunch))
                {
                    punch = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityGrab))
                {
                    grab = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityShockwave))
                {
                    shockwave = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityChargedShot))
                {
                    shockblast = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityWall))
                {
                    wall = ability.Value;
                }
                else if (ability.Value.GetType() == typeof(AbilityRangedGrab))
                {
                    rangedgrab = ability.Value;
                } else if (ability.Value.GetType() == typeof (AbilityBomb))
                {
                    bomb = ability.Value;
                }
            }
        }


        //Check defensive options every frame for quick reaction time
        var obj = GetClosestTargetFromList(_projectiles);
        if (obj
            && Vector3.Distance(transform.position, obj.transform.position) < 3f
            && shield)
        {
            RaycastHit hit;
            if (Physics.BoxCast(obj.transform.position, _data.BoxSize, obj.transform.forward, out hit, obj.transform.rotation))
            {
                if (hit.transform == transform)
                {
                    StartCoroutine(DelayCastAbility(shield, null, .2f));
                    return;
                }
            }
        }

        //Check offensive options only after refresh
        if (!_refreshed)
            return;

        if (_players.Count(x => Vector3.Distance(transform.position, x.transform.position) < 1.5f) > 1 && (shockwave || bomb))
        {

            if (bomb && shockwave)
            {
                var random = Random.Range(0f, 1f);
                if (random > .5f)
                {
                    CastAbility(shockwave);
                    return;
                }
                else
                {
                    CastAbility(bomb);
                    return;
                }
            }
            else
            {
                if (shockwave)
                {
                    CastAbility(shockwave);
                    return;
                }
                if (bomb)
                {
                    CastAbility(bomb);
                    return;
                }
            }
        }

        obj = CheckFrontSide(2f);
        if (obj && _players.Contains(obj) && punch)
        {
            CastAbility(punch);
            return;
        }
            
        obj = CheckFrontSide(5f);
        if (obj && _players.Contains(obj) && wall)
        {
            CastAbility(wall);
            return;
        }

        obj = CheckFrontSide(15f);
        if (obj && _players.Contains(obj) && shockblast)
        {
            StartCoroutine(DelayCastAbility(shockblast, obj, 1f));
            return;
        }

        obj = CheckFrontSide(3f);
        if (dash && obj == null && !OnTarget)
        {
            CastAbility(dash);
            return;
        }
    }

    Vector3 GetOptimalRotation()
    {
        List<Vector3> results = new List<Vector3>();

        var divideValue = 30;
        for (int i = 0; i < (360 / divideValue); i++)
        {
            var rot = transform.rotation * Quaternion.Euler(0, (i * divideValue) + 180f, 0);
            var dir = rot*transform.forward;
            var origin = transform.position + _data.HeightOffset;
            if (!Physics.BoxCast(
                origin,
                _data.BoxSize, 
                dir,
                transform.rotation,
                1f))
            {
                results.Add((transform.rotation * rot).eulerAngles);
            }
        }

        if (results.Count == 0)
        {
            Log("Failed to find new rotation");
            return transform.rotation.eulerAngles;
        }

        return results[UnityEngine.Random.Range(0, results.Count)];
    }

    GameObject GetOptimalTarget()
    {
        GameObject player = GetClosestTargetFromList(_players);
        GameObject pickup = GetClosestTargetFromList(_pickups);
        GameObject objective = GetClosestTargetFromList(_controlPoints);

        var pDis = GetDistance(player);
        var puDis = GetDistance(pickup);
        var oDis = GetDistance(objective);
        
        if (objective != null && oDis < 10f)
        {

            return objective;
        }

        if (pickup != null && puDis < 6f)
        {
            return pickup;
        }

        if (player != null && pDis < 3f && player.GetComponent<Hitpoints>() != null)
        {
            return player;
        }

        

        if (objective != null)
        {
            return objective;
        }

        if (player != null)
        {
            return player;
        }

        return null;
    }

    GameObject GetClosestTargetFromList(IEnumerable<GameObject> list)
    {
        GameObject target = null;
        foreach (var item in list)
        {
            if (item == gameObject)
                continue;

            if (target == null)
            {
                target = item;
            }
            else
            {
                if (Vector3.Distance(transform.position, item.transform.position)
                    < Vector3.Distance(transform.position, target.transform.position))
                {
                    target = item;
                }
            }
        }

        return target;
    }

    GameObject CheckFrontSide(float distance, bool filterPlayer = false)
    {
        GameObject result = null;
        var origin = transform.position + _data.HeightOffset;
        foreach (var hit in Physics.BoxCastAll(origin, _data.BoxSize, transform.forward, transform.rotation, distance))
        {
            if (hit.transform.gameObject == gameObject)
                continue;

            if (filterPlayer && _players.Contains(hit.transform.gameObject))
                continue;

            result = hit.transform.gameObject;
            break;
        }

        return result;
    }

    void CastAbility(Ability ability)
    {
        _abilityHandler.CastAbility(ability);
        //_player.CastAbility(ability); THE OLD WAY
    }

    IEnumerator DelayCastAbility(Ability ability, GameObject obj, float delay)
    {
        if (!ability)
            yield break;
        CastAbility(ability);
        _targetObject = obj;
        _state = AiState.Targeting;

        yield return new WaitForSeconds(delay);
        Log(ability.AbilityName);
        //ability.Release = true;
        _abilityHandler.ReleaseAbility(ability);
        _state = AiState.Regular;
    }

    float GetDistance(GameObject obj)
    {
        if (obj == null)
            return 0f;

        return Vector3.Distance(transform.position, obj.transform.position);
    }

    void Log(string message)
    {
        //TriLog.Log("Ai", _player.Index, message);
    }

    [System.Serializable]
    public class Range
    {
        public float Min;
        public float Max;
    }
}

