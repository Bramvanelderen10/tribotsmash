using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRangedGrab : Ability
{
    public GameObject ProjectilePrefab;
    public Transform Origin; //From where the projectile launches
    public Vector3 StartLocation = new Vector3(1f, 2.6f, 2f);

    public float MaxDistance = 10f; //The max distance of the projectile
    public float DamageDealt = 2f;

    private bool _projectileIsFinished = true;

    public override bool IsReady
    {
        get
        {
            return base.IsReady && _projectileIsFinished;
        }
    }

    protected override bool CanInterrupt { get { return false; } }

    protected override void Start()
    {
        base.Start();
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (!Origin)
        {
            var newObj = new GameObject();
            newObj.transform.parent = transform;
            newObj.transform.localPosition = StartLocation;
            newObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            newObj.name = "ShootOrigin";
            Origin = newObj.transform;
        }
    }

    public override void Cast()
    {
        if (!IsReady || IsCasting)
            return;

        var obj = Instantiate(ProjectilePrefab);
        obj.transform.position = Origin.position;
        obj.transform.rotation = Origin.rotation;

        var comp = obj.GetComponent<GrabProjectile>();
        comp.Origin = transform.parent.gameObject;
        comp.GrabAbility = this;
        comp.Damage = DamageDealt;
        comp.Exectute();
        _projectileIsFinished = false;

        Timestamp = Time.time + Cooldown;

        CastCallBack(this);
    }

    public void Grappled()
    {
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Grappling);
    }

    public void EndProjectile()
    {
        _projectileIsFinished = true;
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
    }

}

