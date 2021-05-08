using System.Linq;
using UnityEngine;
using Tribot;

public class GameModeMeteor : GameMode
{
    [SerializeField]
    private GameObject _meteorPrefab;

    [Header("Default settings")]
    [SerializeField]
    private float _meteorInterval = .5f;
    [MinMaxRange(0f, 20f), SerializeField]
    private Range _meteorCount = new Range() {Min = 0, Max = 1};

    [Header("Difficulty increase over time settings")]
    [SerializeField]
    private float _diffcultyDuraction = 2.5f;
    [SerializeField]
    private float _increaseMeteorCount = .5f;

    private float _timestamp = 0f;
    private float _timestampDifficulty;
    private float _additiveMeteors = 0f;

    private int PlayersAlive
    {
        get
        {
            return Players.Count(player => player.IsAlive);
        }
    }

    void Start()
    {
        
        var follow = GameObject.FindObjectOfType<PerspectiveFollow>();
        follow.ScaleOffset = 4f;
        follow.MinSize = 8f;
    }

    protected override void StartGameMode()
    {
        _timestampDifficulty = Time.time + _diffcultyDuraction;
    }

    protected override void ExtendedUpdate()
    {
        if (!IsFinished && State == GameModeState.Gameplay && IsMaster)
        {
            if (PlayersAlive <= 1)
            {
                EndGameModeSynced();
            }

            if (Time.time > _timestampDifficulty)
            {
                photonView.RPC("UpdateMeteorValues", PhotonTargets.All, _increaseMeteorCount, _diffcultyDuraction);
            }

            if (Time.time > _timestamp)
            {
                var count = Random.Range(_meteorCount.Min + Mathf.FloorToInt(_additiveMeteors), _meteorCount.Max + 1 + Mathf.FloorToInt(_additiveMeteors));

                for (int i = 0; i < count; i++)
                {
                    var pos = PField.GetRandomPosition();

                    photonView.RPC("SpawnMeteor", PhotonTargets.All, pos);
                }

                _timestamp = Time.time + _meteorInterval;
            }
        }
    }

    [PunRPC]
    void UpdateMeteorValues(float addedMeteors, float difficultyDuration)
    {
        _additiveMeteors += addedMeteors;
        _timestampDifficulty = Time.time + difficultyDuration;
    }

    [PunRPC]
    void SpawnMeteor(Vector3 position)
    {
        var obj = Instantiate(_meteorPrefab);
        obj.transform.position = position;
    }


    protected override void ExtendedEndGame()
    {
        foreach (var player in Players)
            PlayerScores[player.Index] = player.IsAlive ? 1 : 0;
    }
}

