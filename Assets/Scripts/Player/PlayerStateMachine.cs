using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class PlayerStateMachine : PunBehaviour
{

    public enum PlayerStates
    {
        UnInitialised,
        Idle,
        Aim,
        ReleaseAim,
        ShockWave,
        Shield,
        Stun,
        KnockDown,
        Grappling,
        Punch,
        Dead,
        Grab,
        Dash,
        Wall,
        Bomb
    }

    private readonly Dictionary<PlayerStates, string> AnimationStateMapper = new Dictionary<PlayerStates, string>()
    {
        {
            PlayerStates.UnInitialised, "Idle"
        },
        {
            PlayerStates.Idle, "Idle"
        },
        {
            PlayerStates.Aim, "Aim"
        },
        {
            PlayerStates.ReleaseAim, "ReleaseAim"
        },
        {
            PlayerStates.ShockWave, "ShockWave"
        },
        {
            PlayerStates.Shield, "Shield"
        },
        {
            PlayerStates.Stun, "Stun"
        },
        {
            PlayerStates.KnockDown, "KnockDown"
        },
        {
            PlayerStates.Grappling, "Grapple"
        },
        {
            PlayerStates.Punch, "Punch"
        },
        {
            PlayerStates.Dead, "Dead"
        },
        {
            PlayerStates.Grab, "Grab"
        },
        {
            PlayerStates.Dash, "Dash"
        },
        {
            PlayerStates.Wall, "Wall"
        },
        {
            PlayerStates.Bomb, "Bomb"
        }
    };

    public PlayerStates State = PlayerStates.UnInitialised;
    [HideInInspector]
    public float Speed = 1f;

    public bool CanInteract
    {
        get { return State == PlayerStates.Idle; }
    }

    public bool CanMove
    {
        get { return StateIsLayered(State) || State == PlayerStates.Dash; }
    }

    public bool CanStun
    {
        get
        {
            return
                CanMove 
                || State == PlayerStates.Shield 
                || State == PlayerStates.Dash;
        }
    }

    private Ability _currentAbility;
    private Animator _anim;

    public Animator Anim
    {
        set { _anim = value; }
    }

    // Use this for initialization
	void Start () {
	    State = PlayerStates.Idle;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        if (_anim == null)
            return;

        _anim.SetFloat("Speed", Speed);
	}

    public bool SwitchState(PlayerStates state, Ability ability = null, float duration = 1f)
    {
        if (_anim == null)
            return false;

        if (State != state) 
        {
            AssignNewAbility(ability);
            if (StateIsLayered(State))
                _anim.SetBool(AnimationStateMapper[PlayerStates.Idle], false);
            _anim.SetBool(AnimationStateMapper[State], false);
            if (StateIsLayered(state))
                _anim.SetBool(AnimationStateMapper[PlayerStates.Idle], true);
            _anim.SetBool(AnimationStateMapper[state], true);
            State = state;
            if (state == PlayerStates.Dead)
            {
                //Destroy(GetComponent<PlayerControlled>());
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().enabled = false;
            }

            return true;
        }

        return false;
    }

    public void SetAnimator(Animator anim)
    {
        _anim = anim;
    }

    void AssignNewAbility(Ability ability)
    {
        if (_currentAbility != null)
        {
            _currentAbility.Cancel();
        }

        _currentAbility = ability;
    }

    static bool StateIsLayered(PlayerStates state)
    {
        return state == PlayerStates.Idle || state == PlayerStates.Aim || state == PlayerStates.Punch || state == PlayerStates.Grab;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Speed);
        }
        else
        {
            //Network player, receive data
            Speed = (float)stream.ReceiveNext();
        }
    }
}
