using UnityEngine;
using System.Collections;
using System;
using Tribot;

public class AbilityChargedShot : Ability
{
    [Header("Settings")]
    public Transform Origin;
    public Vector3 StartLocation = new Vector3(1f, 2.6f, 2f);
    public GameObject ChargedShotPrefab;

    [Header("Charged Shot Stats")]
    public float ChargeRate = 0.5f;
    public float MaxCastTime = 2f;
    public float BaseRadius = 0.2f;
    public float BaseDamage = 0.01f;
    public float BaseKnockbackForce = 50f;
    public float BaseStunDuration = 0.01f;
    public float Speed = 20f;
    public float ReleaseDelay = .5f;

    [HideInInspector]
    public bool IsCharging = false;

    [Header("Audio Clips")]
    public CustomClip Launch;

    private float _damage;
    private float _radius;
    private float _knockbackForce;
    private float _stunDuration;
    private Coroutine cr = null;
    private GameObject _projectile = null;
    private AudioSource _audio;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _audio = gameObject.AddAudioSource();
        if (Origin == null)
	    {
	        Origin = transform.parent;
	    }
	}

    public override void Cast()
    {
        if (!IsReady || IsCasting || !_player.IsOwner)
            return;

        photonView.RPC("RpcCharging", PhotonTargets.All, PhotonNetwork.AllocateViewID());
    }

    [PunRPC]
    void RpcCharging(int viewId)
    {
        _projectile = Instantiate(ChargedShotPrefab);
        _projectile.transform.parent = Origin;
        _projectile.transform.localPosition = StartLocation;
        _projectile.AddComponent<PhotonView>().viewID = viewId;

        _damage = BaseDamage;
        _radius = BaseRadius;
        _knockbackForce = BaseKnockbackForce;
        _stunDuration = BaseStunDuration;
        IsCharging = true;
        IsCasting = true;
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Aim);

        cr = StartCoroutine(StartCharging());

        CastCallBack(this);
    }

    public override bool Cancel()
    {
        if (!IsCasting)
            return false;

        if (IsCharging)
        {
            Destroy(_projectile);
            IsCharging = false;
        }

        StopCoroutine(cr);
        IsCasting = false;
        Timestamp = Time.time + Cooldown;
        _audio.Stop();

        return true;
    }

    // Update is called once per frame
    void Update () {
	    if (IsCharging)
	    {
	        _radius += (ChargeRate / 2) *Time.deltaTime;
	        _damage += (ChargeRate * 4)*Time.deltaTime;
	        _stunDuration += (ChargeRate*2)*Time.deltaTime;
	        _knockbackForce += (ChargeRate*1000)*Time.deltaTime;
	        _projectile.transform.localScale = Vector3.one*(_radius*2);
	    }
	}

    protected override void ExtendedLateUpdate()
    {
        if (!IsCasting)
            return;

        if (Release)
        {
            StopCoroutine(cr);
            ReleaseChargedShot();
        }
    }

    void ReleaseChargedShot()
    {
        IsCharging = false;
        _projectile.transform.localPosition = StartLocation;
        _projectile.transform.parent = null;
        _projectile.transform.rotation = Origin.rotation;
        var comp = _projectile.GetComponent<ShockBlast>();
        comp.Damage = _damage;
        comp.Radius = _radius;
        comp.Origin = Origin;
        comp.Speed = Speed;
        comp.KnockbackForce = _knockbackForce;
        comp.StunDuration = _stunDuration;
        comp.IsReleased = true;
        if (_player.IsOwner)
        {
            comp.Original = true;
        }
        Destroy(_projectile, 10f);
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.ReleaseAim);
        Launch.Play(_audio);

        cr = null;
        cr = StartCoroutine(ReturnToIdle());
    }

    IEnumerator StartCharging()
    {
        
        yield return new WaitForSeconds(MaxCastTime);

        ReleaseChargedShot();
    }

    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(ReleaseDelay);

        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
        Timestamp = Time.time + Cooldown;
        IsCasting = false;
    }
}
