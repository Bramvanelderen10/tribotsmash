using UnityEngine;
using Tribot;

public class AbilityShockwave : Ability
{
    [Header("Others")]
    public LayerMask LMask;
    public GameObject EffectsPrefab;
    public Vector3 ShockWavePosition = new Vector3(0f, 0f, 0.8f);

    [Header("Settings")]
    public float CastTime = 0.2f;
    public float KnockbackDuration =  1f;
    public float Radius = 3f;
    public float Force = 300f;
    public float UpwardForce = 350f;
    public float DamageDealt = 0.5f;

    [Header("Audio")]
    public CustomClip Jump;
    public CustomClip Explosion;

    private Coroutine cr = null;
    private CustomParticleBursts _effects;
    private AudioSource _audio;

    protected override bool CanInterrupt { get { return false; } }

    protected override void Start()
    {
        base.Start();
        _audio = gameObject.AddAudioSource();
        AttachNewEffect();
    }

    protected override void Prepare()
    {
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.ShockWave);
        Jump.Play(_audio);
    }

    protected override void Execute()
    {
        var results = Physics.OverlapSphere(transform.TransformPoint(ShockWavePosition), Radius);
        foreach (var result in results)
        {
            if (result.transform == transform.parent)
                continue;

            var rotation = Quaternion.LookRotation((result.transform.position - transform.position).normalized);
            var direction = rotation * Vector3.forward * Force;
            var force = new Vector3(direction.x, UpwardForce, direction.z);

            if (result.GetComponent<Target>())
            {
                GenericHit(result.GetComponent<Target>().photonView.viewID, DamageDealt, force, KnockbackDuration);
            }
            else
            {
                var rb = result.GetComponent<Rigidbody>();
                if (!rb)
                    continue;
                rb.AddForce(force);
            }
        }

        _effects.transform.parent = null;
        _effects.transform.position = transform.TransformPoint(ShockWavePosition);
        _effects.Execute();
        AttachNewEffect();

        Explosion.Play(_audio);
    }

    private void AttachNewEffect()
    {
        var obj = Instantiate(EffectsPrefab);
        obj.transform.parent = transform;
        _effects = obj.GetComponent<CustomParticleBursts>();
    }
}
