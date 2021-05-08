using UnityEngine;
using Tribot;

public class AbilityShield : Ability
{
    [Header("Settings")]
    public float BaseScale = 6f;
    public float MaxCapacity = 20f;
    public float ChargeRate = 0.5f; //How fast the shield will recharge when not casting in percentages
    public float DrainRate = 0.5f; //How fast the shield will drain in percentages
    public float StunDuration = 0.5f;
    public float CastCost = 0.1f; //How much casting the shield cost in percentages
    public Vector3 ShieldPosition = new Vector3(0f, 1.25f, 0f);
    public CustomClip ShieldClip;

    private CapsuleCollider _parentCollider;
    private Collider _collider;
    private ShieldHitpoints _hitpoints;
    private Shield _shield;
    private GameObject child;
    private AudioSource _audio;

    private float ScaleMultiplier
    {
        get { return _hitpoints.Hitpoints/MaxCapacity; }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        child = transform.GetChild(0).gameObject;
	    child.transform.localPosition = ShieldPosition;

        _parentCollider = transform.parent.GetComponent<CapsuleCollider>();
	    _hitpoints = child.AddComponent<ShieldHitpoints>();
	    _hitpoints.Hitpoints = MaxCapacity;
	    _shield = child.GetComponent<Shield>();
	    _shield.BaseScale = BaseScale;
        _shield.ScaleMultiplier = ScaleMultiplier;
        child.SetActive(false);
        
	    var capsule = (CapsuleCollider) gameObject.AddComponent<CapsuleCollider>(_parentCollider);
	    capsule.radius /= 2;
	    capsule.height /= 2;
	    capsule.center /= 2;
	    _collider = capsule;
        _collider.enabled = false;

	    _audio = gameObject.AddAudioSource();
    }

    public override void Cast()
    {
        if (IsCasting || !IsReady)
            return;

        _hitpoints.Hitpoints -= CastCost * MaxCapacity;

        _shield.ScaleMultiplier = ScaleMultiplier;
        child.transform.localScale = Vector3.one;
        child.SetActive(true);
        _collider.enabled = true;
        _parentCollider.enabled = false;

        IsCasting = true;
        ShieldClip.Play(_audio);
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Shield);

        CastCallBack(this);
    }

    public override bool Cancel()
    {
        if (!IsCasting)
            return false;

        child.SetActive(false);
        _collider.enabled = false;
        _parentCollider.enabled = true;
        IsCasting = false;
        _audio.Stop();

        return true;
    }

    // Update is called once per frame
    void Update () {
	    if (IsCasting)
	    {
	        _hitpoints.Hitpoints -= DrainRate*MaxCapacity*Time.deltaTime;

	        if (!_hitpoints.IsAlive)
	        {
	            _player.Stun(StunDuration);
	            _hitpoints.Hitpoints = MaxCapacity;
	        }
            _shield.ScaleMultiplier = ScaleMultiplier;
	    }
	    else
	    {
	        if (_hitpoints.Hitpoints < MaxCapacity)
	        {
                _hitpoints.Hitpoints += ChargeRate * MaxCapacity * Time.deltaTime;
            }
	    }
    }

    protected override void ExtendedLateUpdate()
    {
        if (!IsCasting)
            return;

        if (Release)
        {
            TriLog.Log("Shield up");
            PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
            Cancel();
        }
    }
}
