using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tribot;

/// <summary> 
/// The base gamemode class sets up the game state, players, pickups and ingame UI
/// It also managed the player scores which are determine by the objective of the current gamemode
/// To create a new gamemode extend from this class
/// </summary>
/// 
public abstract class GameMode : TribotBehaviour
{
    protected enum GameModeState
    {
        UnInitialized,
        PreGame, //When the round just started player have 5 seconds to join on untill the gamemode starts
        Gameplay //The gamemode has started and the players can play
    }
    

    [Header("GameMode Settings")]
    [SerializeField]
    private string GameModeName;
    [SerializeField]
    private List<AbilityTypes> StarterAbilities; //The starter set of abilities
    [SerializeField]
    private bool EnablePickUps = true;
    [MinMaxRange(1f, 15f), SerializeField]
    private Range PickUpInterval;
    [SerializeField]
    private AudioClip IntroductionClip;

    //Other variables hidden from unity inspector
    public Dictionary<int, float> PlayerScores { get; set; }
    public bool IsFinished { get; private set; }

    protected Playfield PField;
    protected PickUpManager Pm;
    protected List<IPlayerManager> Players;
    protected GameModeState State = GameModeState.UnInitialized;

    [SerializeField] private GameModeData _data;
    private float _startTime = 0f;
    private float RemainingTime
    {
        get { return _startTime - Time.time; }
    }

    void Update()
    {
        if (State == GameModeState.PreGame)
        {
            //If the pregame time runs out start the gamemode
            if (RemainingTime <= 0f)
            {
                State = GameModeState.Gameplay;

                PrepareForStart();
                StartGameMode();

                if (EnablePickUps)
                    Pm.Enable();
            }

            //Designed for Local play. Checks if a player wants to join or drop out during pregame
            if (PhotonNetwork.offlineMode)
            {
                for (var i = 0; i < TribotInput.InputCount; i++)
                {
                    if (TribotInput.GetButtonDown(TribotInput.InputButtons.Start, i))
                    {
                        if (!Players.Any(x => !x.Ai && x.InputIndex == i))
                        {
                            var player = Players.First(x => x.Ai);
                            player.Ai = false;
                            player.InputIndex = i;
                            player.CreatePlayer(PField.GetSpawnLocation(player.Index));
                        }
                    }

                    if (TribotInput.GetButtonDown(TribotInput.InputButtons.Back, i))
                    {
                        if (Players.Any(x => !x.Ai && x.InputIndex == i))
                        {
                            var player = Players.First(x => !x.Ai && x.InputIndex == i);
                            player.Ai = true;
                            player.CreatePlayer(PField.GetSpawnLocation(player.Index));
                        }
                    }
                }
            }
            
        }

        if (State == GameModeState.Gameplay)
        {
            ExtendedUpdate();
        }
    }

    bool UpdateTime()
    {
        //TODO UPDATE UI HERE

        return true;
    }

    public virtual void Initialize(Playfield pf)
    {
        IsFinished = false;
        PField = pf;
        Players = Container<IPlayerManager>.Instance.Items;
        PlayerScores = new Dictionary<int, float>();

        for (var i = 0; i < Players.Count; i++)
        {
            PlayerScores.Add(Players[i].Index, 0f);
            Players[i].CreatePlayerCallback += PlayerCreated;
            //Players[i].CreatePlayer(PField.GetSpawnLocation(i));
        }
        
        Pm = PickUpManager.Instance;
        Pm.PickUpInterval = PickUpInterval;
        Pm.BorderLayer = _data.BorderLayer;
        Pm.GroundLayer = _data.GroundLayer;
        _startTime = Time.time + _data.PreGameDuration;
        InstantiateUi();
        State = GameModeState.PreGame;
    }

    public virtual void InstantiateUi()
    {
        var obj = Instantiate(_data.GameModeIntruction);
        var intro = obj.GetComponent<GameModeIntroduction>();
        intro.IntroductionSound = IntroductionClip;
        intro.IntroductionText = GameModeName;
        Destroy(obj, _data.PreGameDuration);
    }
    
    protected void EndGameModeSynced()
    {
        photonView.RPC("EndGameMode", PhotonTargets.All);
    }

    [PunRPC]
    public void EndGameMode()
    {
        if (Pm)
            Pm.Disable();

        IsFinished = true;

        ExtendedEndGame();

        foreach (var player in Players)
        {
            player.CreatePlayerCallback -= PlayerCreated;
            player.DestroyPlayer();
        }
    }

    //RUN ONLY AFTER PREGAME STATE
    protected virtual void StartGameMode() { }
    protected virtual void ExtendedUpdate() { }
    protected virtual void ExtendedEndGame() { }
    protected virtual void PlayerCreated(IPlayerManager player) { }

    //Returns the winning player interfaces
    public List<int> GetWinners()
    {
        var result = PlayerScores.Max(player => player.Value);

        var results = new List<int>();
        foreach (var player in PlayerScores)
        {
            if (player.Value.Equals(result))
            {
                results.Add(Players.Find(p => p.Index == player.Key).Index);
            }
        }

        return results;
    }

    /// <summary>
    /// Runs prior to StartGamemode 
    /// override this method if something needs to be prepared on every client 
    /// right before the countdown ends
    /// </summary>
    [PunRPC]
    protected virtual void PrepareForStart()
    {
        foreach (var player in Players)
        {
            foreach (var ability in StarterAbilities)
            {
                player.AddAbility(ability);
            }
        }
    }
}

