
using UnityEngine;
using Tribot;

public class AbilityPunch : Ability
{
    //Variables
    [Header("Settings")]
    public float KnockBackForce  = 500f;
    public float Damage = 1f;

    [Header("Forward Check")]
    public Vector3 HitBoxSize = new Vector3(0.5f, 0.55f, 0.5f);
    public Vector3 HitBoxCenter = new Vector3(0f, 1.2f, 0.8f);

    [Header("Hit Settings")]
    public CustomClip ClipPunch;
    public CustomClip ClipHit;
    public GameObject HitEffects;

    private AudioSource _audio;
    private AudioSource _audio2;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _audio = gameObject.AddAudioSource();
        _audio2 = gameObject.AddAudioSource();
    }

    protected override void Prepare()
    {
        ClipPunch.Play(_audio2);
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Punch, this);
    }

    protected override void Execute()
    {
        var center = transform.parent.TransformPoint(HitBoxCenter);
        var hit = false;
        foreach (var coll in Physics.OverlapBox(center, HitBoxSize, transform.rotation))
        {
            if (coll.isTrigger)
                return;

            if (coll.transform == transform.parent)
                continue;

            hit = true;
            var obj = coll.gameObject;

            if (obj.GetComponent<Target>())
            {
                GenericHit(obj.GetComponent<Target>().photonView.viewID, Damage, Vector3.zero, 0f, false);
            }
            else
            {
                if (obj.GetComponent<Rigidbody>())
                    obj.GetComponent<Rigidbody>().AddForce(transform.forward * KnockBackForce);
            }


            var pos = transform.position;
            pos.y += 1f;
            var dir = obj.transform.position - transform.position;
            dir = dir / dir.magnitude;
            dir.y = 0f;
            foreach (var result in Physics.RaycastAll(pos, dir, 30f))
            {
                if (result.collider != coll || !HitEffects)
                    continue;
                var objFx = Instantiate(HitEffects);
                objFx.transform.position = result.point;
            }
        }

        if (hit)
        {
            ClipHit.Play(_audio);
        }
    }
}
