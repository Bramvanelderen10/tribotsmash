using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tribot;

/// <summary>
/// The player class is responsible for the player object in the game itself
/// It manages ability usage and can get called to apply a state to a player externally
/// 
/// Derive from this class to add control methods, either AI or Player controlled
/// </summary>
public class Player : Target
{
    
    [HideInInspector]
    public bool IsAlive = true;
    [HideInInspector]
    public Ability CurrentAbility = null;

    public int Index
    {
        get
        {
            if (_info)
            {
                return _info.Index;
            }
            else
            {
                return -1;
            }
        }
    }

    public bool IsOwner
    {
        get { return _info.IsOwner; }
    }

    public float SpeedMultiplier { get; private set; }
    public Dictionary<TribotInput.InputButtons, Ability> Abilities { get; set; }

    public List<AbilityTypes> TESTINGONLYStarterAbilities = new List<AbilityTypes>();


    private PlayerStateMachine _state;
    private Rigidbody _rb;
    private PlayerInfo _info;

    private Coroutine _stunRoutine;
    [SerializeField]
    private float _knockbackDuration = 1.2f;


    private AbilityData _data;


    void Start ()
	{
        TargetContainer.Instance.Players.Add(gameObject);
        _data = Resources.Load<AbilityData>("AbilityData");
        _info = GetComponent<PlayerInfo>();
        _state = GetComponent<PlayerStateMachine>();
	    _rb = GetComponent<Rigidbody>();
        Abilities = new Dictionary<TribotInput.InputButtons, Ability>();
        SpeedMultiplier = 1f;
	    TESTINGONLYAddStarterAbilities();
	}

	void LateUpdate ()
	{
	    if (CurrentAbility == null || !CurrentAbility.IsCasting)
	    {
	        SpeedMultiplier = 1f;
	    }
	    
	}

    void FixedUpdate()
    {

    }

    void OnDestroy()
    {
        TargetContainer.Instance.Players.Remove(gameObject);
    }

    public void CastAbility(Ability ability)
    {
        if (!Abilities.ContainsValue(ability))
            return;

        ability.Cast();
    }

    /// <summary>
    /// If cast successfull this method gets executed
    /// </summary>
    /// <param name="ability"></param>
    public void CastEvent(Ability ability)
    {
        SpeedMultiplier = ability.CastMSMultiplier;
        CurrentAbility = ability;

        SpeedMultiplier = ability.CastMSMultiplier;
    }

    public override bool Stun(float duration, bool forceStun = false)
    {
        if (_state.CanStun || forceStun)
        {
            _stunRoutine = StartCoroutine(StunPlayerRoutine(duration));
            base.Stun(duration);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CanStun()
    {
        return _state.CanStun;
    }

    private IEnumerator StunPlayerRoutine(float duration)
    {
        _state.SwitchState(PlayerStateMachine.PlayerStates.Stun);
        if (CurrentAbility != null)
        {
            CurrentAbility.Cancel();
        }

        yield return new WaitForSeconds(duration);
        ClearStun();
    }

    public void ClearStun()
    {
        if (_state.State == PlayerStateMachine.PlayerStates.Stun)
            _state.SwitchState(PlayerStateMachine.PlayerStates.Idle);
        if (_stunRoutine != null)
            StopCoroutine(_stunRoutine);
    }

    public override void KnockDown(Vector3 force, Transform lookAt)
    {
        base.KnockDown(force, lookAt);

        if (!_state.CanStun)
            return;
        if (_rb)
            _rb.AddForce(force);
        transform.LookAt(lookAt);
        transform.LookAt(new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z));

        _state.SwitchState(PlayerStateMachine.PlayerStates.KnockDown);
        StartCoroutine(StopKnockDown(_knockbackDuration));
        if (CurrentAbility != null)
        {
            CurrentAbility.Cancel();
        }
    }
    
    public override void KnockDown()
    {
        base.KnockDown();

        _state.SwitchState(PlayerStateMachine.PlayerStates.KnockDown);
        StartCoroutine(StopKnockDown(_knockbackDuration));
        if (CurrentAbility != null)
        {
            CurrentAbility.Cancel();
        }
    }

    public override void KnockDown(Vector3 force, Transform lookAt, float delay, bool velocity = false, bool forceKnock = false)
    {
        if (!_state.CanStun && !forceKnock)
            return;

        transform.LookAt(lookAt);
        transform.LookAt(new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z));
        KnockDown(force, delay, velocity);
    }

    public override void KnockDown(Vector3 force, float delay, bool velocity = false)
    {
        if (!_state.CanStun)
            return;

        if (_rb)
        {
            if (velocity)
                _rb.velocity = force;
            else
                _rb.AddForce(force);
        }

        _state.SwitchState(PlayerStateMachine.PlayerStates.KnockDown);
        StartCoroutine(StopKnockDown(delay));
        if (CurrentAbility != null)
        {
            CurrentAbility.Cancel();
        }
    }

    private IEnumerator StopKnockDown(float delay)
    {
        yield return new WaitForSeconds(delay);

        _state.SwitchState(PlayerStateMachine.PlayerStates.Idle);
    }

    public override void Slow(float duration, float amount)
    {
        base.Slow(duration, amount);

        StartCoroutine(SlowPlayerRoutine(duration, amount));
    }

    private IEnumerator SlowPlayerRoutine(float duration, float speedMultiplier)
    {
        SpeedMultiplier = speedMultiplier;
        yield return new WaitForSeconds(duration);
        SpeedMultiplier = 1f;
    }

    public override void Kill()
    {
        base.Kill();

        _state.SwitchState(PlayerStateMachine.PlayerStates.Dead);
        IsAlive = false;
    }

    public void AddAbility(AbilityTypes abilityType)
    {
        if (!IsOwner)
            return;

        photonView.RPC("RpcAddAbility", PhotonTargets.AllBuffered, abilityType, PhotonNetwork.AllocateViewID());
    }

    [PunRPC]
    void RpcAddAbility(AbilityTypes type, int viewID)
    {
        var prefab = _data.GetAbilityPrefab(type);
        if (!prefab)
            return;

        var obj = Instantiate(prefab);
        obj.transform.parent = transform;
        obj.transform.position = transform.position;
        obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        var comp = obj.GetComponent<Ability>();

        if (!comp)
        {
            Destroy(obj);
            return;
        }

        obj.name = comp.AbilityName;

        if (Abilities == null)
        {
            Abilities = new Dictionary<TribotInput.InputButtons, Ability>();
        }

        if (Abilities.ContainsKey(comp.MappedButton))
        {
            var oldAbility = Abilities[comp.MappedButton];
            Abilities[comp.MappedButton] = comp;

            if (oldAbility != null)
            {
                Destroy(oldAbility.gameObject);
            }
        }
        else
        {
            Abilities.Add(comp.MappedButton, comp);
        }
        comp.PlayerState = _state;
        comp.PlayerIndex = Index;
        comp.CastCallBack = CastEvent;

        var view = obj.AddComponent<PhotonView>();
        view.viewID = viewID;
    }

    void TESTINGONLYAddStarterAbilities()
    {
        foreach (var abiltiy in TESTINGONLYStarterAbilities)
        {
            AddAbility(abiltiy);
        }
    }
}
