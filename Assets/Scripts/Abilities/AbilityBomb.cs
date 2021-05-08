using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tribot;

public class AbilityBomb : Ability
{
    [Header("Bomb Settings")]
    public Vector3 JumpForce = new Vector3(0f, 10f, 5f);
    public Vector3 BombPositionalOffset = new Vector3(0f, 1f, 0f);
    public GameObject BombPrefab;

    [Header("Audio Settings")]
    public CustomClip ClipJump;
    public CustomClip ClipLand;

    private Rigidbody _rb;
    private AudioSource _audio;
    private Bomb _bomb = null;

    protected override bool CanInterrupt
    {
        get { return false; }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _rb = transform.parent.GetComponent<Rigidbody>();
        _audio = gameObject.AddAudioSource();
    }

    protected override void Prepare()
    {
        var obj = Instantiate(BombPrefab);
        obj.transform.position = transform.parent.position + BombPositionalOffset;
        _bomb = obj.GetComponent<Bomb>();

        ClipJump.Play(_audio);

        _rb.velocity += (transform.rotation * JumpForce);

        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Bomb);
    }

    protected override void Execute()
    {
        if (_bomb)
            _bomb.TriggerExplosion(transform.parent.gameObject);
    }

}
