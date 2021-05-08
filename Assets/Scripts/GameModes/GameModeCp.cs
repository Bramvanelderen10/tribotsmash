using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameModeCp : GameMode {

    [SerializeField]
    private float _maxScore = 100f;
    [SerializeField]
    private float _scoreRate = 5f;
    [SerializeField]
    private float _objectiveLifetime = 20f;
    [SerializeField]
    private float _maxGameLength = 300f;
    [SerializeField]
    private GameObject _objectivePrefab;
    
    private ControlPoint _cp;
    private Coroutine _cr;
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

    protected override void PrepareForStart()
    {
        base.PrepareForStart();

        for (int i = 0; i < Players.Count; i++)
        {
            _playerScores.Add(0f);
        }
        _endTime = Time.time + _maxGameLength;
    }

    protected override void StartGameMode()
    {
        StartCoroutine(ControlPointRenewal());
    }

    protected override void ExtendedUpdate()
    {
        if (!IsFinished && State == GameModeState.Gameplay && IsMaster)
        {
            if (_isDone)
            {
                photonView.RPC("EndControlPointMode", PhotonTargets.All);
            }
        }
        
    }

    [PunRPC]
    void EndControlPointMode()
    {
        StopCoroutine(_cr);
        Destroy(_cp.gameObject);
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

    IEnumerator ControlPointRenewal()
    {
        if (IsMaster)
            photonView.RPC("CreateControlPoint", PhotonTargets.All, PField.GetObjectiveSpawnLocation());
        yield return new WaitForSeconds(_objectiveLifetime);
        if (IsMaster)
            photonView.RPC("DestroyControlPoint", PhotonTargets.All);
        _cr = StartCoroutine(ControlPointRenewal());
    }

    [PunRPC]
    void CreateControlPoint(Vector3 position)
    {
        var obj = Instantiate(_objectivePrefab);
        obj.name = "ControlPoint";
        obj.transform.position = position;

        _cp = obj.GetComponent<ControlPoint>();
        _cp.Initialise(_objectiveLifetime, _scoreRate, AddPoints);
    }

    [PunRPC]
    void DestroyControlPoint()
    {
        if (_cp != null)
            Destroy(_cp.gameObject);
    }
}
