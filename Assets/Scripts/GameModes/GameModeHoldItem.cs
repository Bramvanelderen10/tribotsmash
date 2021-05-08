using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameModeHoldItem : GameMode
{
    [SerializeField] private float _maxScore = 100f;
    [SerializeField] private float _scoreRate = 2f;
    [SerializeField] private float _scoreInterval = 0.5f;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private float _maxGameLength = 300f;
    [SerializeField] private GameObject _ItemPrefab;

    private GameObject _item;
    private List<float> _playerScores = new List<float>();
    private float _endTime = 0f;

    private bool _isDone
    {
        get { return _endTime - Time.time <= 0f || _playerScores.Max(x => x) >= _maxScore; }
    }

    protected override void ExtendedEndGame()
    {
        for (int i = 0; i < PlayerScores.Count; i++)
        {
            PlayerScores[i] = _playerScores[i];
        }
    }

    protected override void StartGameMode()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            _playerScores.Add(0f);
        }
        _endTime = Time.time + _maxGameLength;

        if (!IsMaster)
            return;

        photonView.RPC("SpawnItem", PhotonTargets.All, PField.GetObjectiveSpawnLocation(),
            PhotonNetwork.AllocateViewID());
    }

    [PunRPC]
    void SpawnItem(Vector3 pos, int viewId)
    {
        _item = Instantiate(_ItemPrefab);
        _item.transform.position = pos;
        var comp = _item.GetComponent<ObjectiveItem>();
        comp.Initialise(_scoreRate, _scoreInterval, _cooldown, AddPoints);
        comp.photonView.viewID = viewId;
        comp.photonView.TransferOwnership(PhotonNetwork.masterClient);
    }

    protected override void ExtendedUpdate()
    {
        if (!IsFinished && State == GameModeState.Gameplay && IsMaster)
        {
            if (_isDone)
            {
                photonView.RPC("EndItemMode", PhotonTargets.All);

            }
        }
    }

    [PunRPC]
    void EndItemMode()
    {
        Destroy(_item.gameObject);
        EndGameMode();
    }

    public void AddPoints(int playerIndex, float score)
    {
        if (!IsMaster)
            return;

        photonView.RPC("UpdateScore", PhotonTargets.All, playerIndex, score);
    }

    [PunRPC]
    public void UpdateScore(int playerIndex, float score)
    {
        if (playerIndex < _playerScores.Count)
        {
            _playerScores[playerIndex] += score;
        }
    }
}
