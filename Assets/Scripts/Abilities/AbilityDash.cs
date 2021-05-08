using UnityEngine;
using Tribot;

public class AbilityDash : Ability
{
    [Header("Dash Settings")]
    public float DashDistance = 2f;
    public float DashSpeed = 3f;
    public float DamageDealt = 2f;
    public float KnockbackDuration = .3f;
    public GameObject DashFx;

    [Header("Forward Casting Settings")]
    public Vector3 HitBoxSize = new Vector3(0.5f, 0.55f, 0.3f);
    public Vector3 HitBoxCenter = new Vector3(0f, 1.2f, 0.5f);

    [Header("Audio Settings")]
    public CustomClip ClipDash;
    public CustomClip ClipHit;

    private Vector3 _origin = Vector3.zero;
    private DashFx _dashFx;
    private Rigidbody _rb;

    private Vector3 _lastPosition;
    private float _distanceTravelled = 0f;

    private AudioSource _audio;
    private AudioSource _audio2;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        var obj = Instantiate(DashFx);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(0, 1.2f, 0);
        obj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        _dashFx = obj.GetComponent<DashFx>();
        _dashFx.EmittersEnabled = false;
        _rb = transform.parent.GetComponent<Rigidbody>();
        
        _audio = gameObject.AddAudioSource();
        _audio2 = gameObject.AddAudioSource();
    }

    public override void Cast()
    {
        if (IsCasting || !IsReady)
            return;

        ClipDash.Play(_audio);

        _origin = transform.parent.position;
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Dash);
        IsCasting = true;
        _dashFx.EmittersEnabled = true;

        _lastPosition = transform.position;
        _distanceTravelled = 0f;

        CastCallBack(this);
    }

    public override bool Cancel()
    {
        if (!IsCasting)
            return false;

        IsCasting = false;
        _dashFx.EmittersEnabled = false;
        Timestamp = Time.time + Cooldown;

        return true;
    }

    protected override void ExtendedLateUpdate()
    {
        if (_distanceTravelled >= DashDistance)
        {
            StopDash();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
	    if (IsCasting)
	    {
	        var point1 = transform.TransformPoint(new Vector3(0, 1.01f + 0.3f, 0.1f));
            var point2 = transform.TransformPoint(new Vector3(0, 1.01f - 0.3f, 0.1f));

            var results = 
                Physics.OverlapCapsule(point1, point2, 0.6f);

	        foreach (var item in results)
	        {
	            if (item.transform != transform.parent)
	            {
	                if (item.isTrigger)
	                    continue;

	                StopDash();
	                ClipHit.Play(_audio2);

                    var result = item.transform.gameObject;
                    var rotation = Quaternion.LookRotation((result.transform.position - transform.position).normalized);
                    var force = rotation * Vector3.forward;
                    
                    var target = result.GetComponent<Target>();
	                if (target != null)
	                {
	                    GenericHit(target.photonView.viewID, DamageDealt, force, KnockbackDuration);
	                }
	                else
	                {
                        var rb = result.GetComponent<Rigidbody>();
                        if (!rb)
                            continue;
                        rb.AddForce(force);
                    }
                }
	        }
	        _lastPosition = _rb.position;
	        _rb.MovePosition(transform.parent.position + transform.forward*DashSpeed*Time.deltaTime);
	        _distanceTravelled += Vector3.Distance(_lastPosition, _rb.position);
	    }
	}

    void StopDash()
    {
        if (!IsCasting)
            return;

        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
        IsCasting = false;
        _dashFx.EmittersEnabled = false;
        Timestamp = Time.time + Cooldown;
    }
}
