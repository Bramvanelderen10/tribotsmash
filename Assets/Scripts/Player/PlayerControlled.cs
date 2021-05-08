using Tribot;
using UnityEngine;

/// <summary>
/// Handles the player input for ability usage
/// </summary>
public class PlayerControlled : MonoBehaviour
{
    public int InputIndex = 0;

    private PlayerInfo _info;
    private Player _player;
    private PlayerStateMachine _state;
    private PlayerAbilityHandler _abilityHandler;

    /// <summary>
    /// Movement Variables
    /// </summary>
    private Rigidbody _rb;
    private Vector3 _movement;
    private Vector2 _leftStick;
    private float _deadzone = 0.25f;
    private float _baseSpeed = 6f;
    private float Speed
    {
        get { return _baseSpeed * _player.SpeedMultiplier; }
    }

    void Start()
    {
        _info = GetComponent<PlayerInfo>();
        _player = GetComponent<Player>();
        _state = GetComponent<PlayerStateMachine>();
        _rb = GetComponent<Rigidbody>();
        _abilityHandler = GetComponent<PlayerAbilityHandler>();
    }

    void Update()
    {
        int index;
        if (PhotonNetwork.offlineMode)
            index = InputIndex;
        else if ((int) PhotonNetwork.player.CustomProperties["Index"] == _info.Index)
            index = -1;
        else
            return;

        if (_player.CurrentAbility)
        {
            if (TribotInput.GetButtonUp(_player.CurrentAbility.MappedButton, index))
            {
                _abilityHandler.ReleaseAbility(_player.CurrentAbility);
            }
        }

        if (_state.CanInteract)
        {
            foreach (var ability in _player.Abilities)
            {
                if (TribotInput.GetButtonDown(ability.Key, index))
                {
                    _abilityHandler.CastAbility(ability.Value);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!_state.CanMove)
            return;

        if (PhotonNetwork.offlineMode)
        {
            _leftStick = TribotInput.GetAxis(TribotInput.Axis.LeftStick, InputIndex);
            Move(_leftStick);
            Turn(_leftStick);
        }
        else if ((int)PhotonNetwork.player.CustomProperties["Index"] == _info.Index)
        {
            _leftStick = TribotInput.GetAxis(TribotInput.Axis.LeftStick, -1);
            Move(_leftStick);
            Turn(_leftStick);
        }
    }

    private void Move(Vector3 axis)
    {
        if (axis.magnitude < _deadzone)
            axis = Vector3.zero;

        // Set the movement vector based on the axis input.
        _movement.Set(axis.x, 0f, axis.y);
        _movement = Vector3.ClampMagnitude(_movement, 1f);
        _state.Speed = (_movement.magnitude * Speed) / _baseSpeed;
        _movement = _movement * Speed * Time.deltaTime;
        _rb.MovePosition(transform.position + _movement);
    }

    private void Turn(Vector3 axis)
    {
        var lookRotation = _rb.rotation;

        if (!(axis.magnitude < _deadzone))
        {
            var rbEuler = _rb.rotation.eulerAngles;
            var euler = new Vector3(rbEuler.x, (Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg), rbEuler.z);
            lookRotation = Quaternion.Euler(euler);
        }
        _rb.MoveRotation(lookRotation);
    }
}

