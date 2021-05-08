using UnityEngine;
using System;
using System.Collections.Generic;
using Tribot;
using Photon;

/// <summary>
/// 
/// </summary>
public class PlayerAbilityHandler : PunBehaviour
{
    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    public void CastAbility(Ability ability)
    {
        HandleInputDown(ability.MappedButton);
        //_player.CastAbility(ability); THE OLD WAY
    }

    public void ReleaseAbility(Ability ability)
    {
        HandleInputUp(ability.MappedButton);
    }

    void HandleInputDown(TribotInput.InputButtons input)
    {
        photonView.RPC("SyncedInputDown", PhotonTargets.All, input);
    }

    void HandleInputUp(TribotInput.InputButtons input)
    {
        photonView.RPC("SyncedInputUp", PhotonTargets.All, input);
    }

    [PunRPC]
    void SyncedInputUp(TribotInput.InputButtons input)
    {
        _player.Abilities[input].Release = true;
    }

    [PunRPC]
    void SyncedInputDown(TribotInput.InputButtons input)
    {
        var ability = _player.Abilities[input];
        if (ability)
        {
            _player.CastAbility(ability);
        }
    }
}