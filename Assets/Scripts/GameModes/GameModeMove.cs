using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tribot;

public class GameModeMove : GameMode
{
    [SerializeField] private float _playerHealth = 10f;
    [SerializeField] private float _damageRate = .5f;
    [SerializeField] private GameObject _healthbarPrefab;
    private Dictionary<int, PlayerHitpoints> _playerHitpoints;
    private Rigidbody[] _playerRbs = new Rigidbody[4];

    private int PlayersAlive
    {
        get
        {
            return _playerHitpoints.Where(player => player.Value != null).Count(player => player.Value.IsAlive);
        }
    }

    protected override void StartGameMode()
    {
        if (!IsMaster)
            return;

        foreach (var player in Players)
        {
            photonView.RPC("AddHitpoints", PhotonTargets.AllBuffered, player.Index);
        }
    }

    protected override void ExtendedUpdate()
    {
        if (!IsFinished && IsMaster)
        {
            foreach (var player in Players)
            {
                if (_playerHitpoints[player.Index] == null && player.IsEnabled)
                {
                    photonView.RPC("AddHitpoints", PhotonTargets.AllBuffered, player.Index);
                }
                if (_playerRbs[player.Index] == null && player.IsEnabled)
                {
                    _playerRbs[player.Index] = player.PlayerObject.GetComponent<Rigidbody>();
                }
                if (_playerRbs[player.Index] != null)
                {
                    if (_playerRbs[player.Index].velocity.magnitude < .1)
                    {
                        _playerHitpoints[player.Index].AddDamage(_damageRate*Time.deltaTime);
                    }
                }

            }

            if (PlayersAlive <= 1)
            {
                EndGameModeSynced();
            }
        }
    }

    protected override void ExtendedEndGame()
    {
        PlayerScores[GetHighestHitpointsPlayer().Index] = 1;
    }

    protected override void PlayerCreated(IPlayerManager player)
    {
        base.PlayerCreated(player);

        if (!IsMaster)
            return;

        photonView.RPC("AddHitpoints", PhotonTargets.All, player.Index);
    }

    [PunRPC]
    void AddHitpoints(int index)
    {
        if (_playerHitpoints == null)
            _playerHitpoints = new Dictionary<int, PlayerHitpoints>();

        if (!_playerHitpoints.ContainsKey(index))
            _playerHitpoints.Add(index, null);

        if (_playerHitpoints[index] != null)
            return;

        var player = Container<IPlayerManager>.Instance.Items.Find(x => x.Index == index);
        var obj = player.PlayerObject;
        var hp = obj.AddComponent<PlayerHitpoints>();
        if (hp)
        {
            _playerHitpoints[player.Index] = hp;
            _playerHitpoints[player.Index].Hitpoints = _playerHealth;
            _playerHitpoints[player.Index].DisplayHpBar = true;
            _playerHitpoints[player.Index].HealthBarPrefab = _healthbarPrefab;

        }
        else
        {
            _playerHitpoints[player.Index] = null;
        }
    }

    private IPlayerManager GetHighestHitpointsPlayer()
    {
        var query = from player in _playerHitpoints
                    where player.Value != null
            orderby player.Value.Hitpoints descending
            select player.Key;

        return Players[query.First()];
    }
}

