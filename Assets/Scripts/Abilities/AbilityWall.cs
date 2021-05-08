using UnityEngine;
using Tribot;

public class AbilityWall : Ability
{
    [Header("Prefab")]
    public GameObject WallPrefab;
    public GameObject EffectsPrefab;

    [Header("Audio")]
    public CustomClip Jump;
    public CustomClip Explosion;

    private AudioSource _audio;

    protected override bool CanInterrupt { get { return false; } }

    protected override void Start()
    {
        base.Start();
        _audio = gameObject.AddAudioSource();
    }

    protected override void Prepare()
    {
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Wall);
        Jump.Play(_audio);
    }

    protected override void Execute()
    {
        var obj = Instantiate(WallPrefab);
        obj.transform.position = transform.parent.position;
        obj.transform.rotation = transform.parent.rotation;
        obj.GetComponent<Wall>().StartSpawning();
    }
}
