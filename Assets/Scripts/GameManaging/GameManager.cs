using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Tribot;

/// <summary>
/// The GameManager handles all logic that happends outside the rounds while the game is in progress
/// </summary>
public class GameManager : TribotBehaviour
{
    public enum GameState
    {
        Creation, //State while game manager is still being configured
        Gameplay, //State while round is playing
        Scoreboard, //State after round is over
        Endgame, //Final state after all rounds are completed TODO not yet implemented
    }

    private int _playerCount = 4;
    private int _totalRounds = 10;
    private int _roundTime = 120;

    private GameState _state = GameState.Creation;
    private int _currentRound = 0;
    private List<string> _levels;
    private GameModeCollection _GameModeCollection;
    private LevelCollection _LevelCollection;
    private bool _initialized = false;
    private InGameUi _inGameUi;

    private GameMode _gm;

    private List<GameObject> _cleanUpList = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (PhotonNetwork.masterClient == null)
	    {
	        foreach (var player in PhotonNetwork.playerList)
	        {
	            if (player != null)
	                PhotonNetwork.SetMasterClient(player);
	        }
	    }

	    if (!IsMaster)
	        return;

	    if (_state == GameState.Gameplay)
	    {
	        if (_gm && _gm.IsFinished)
	        {
                photonView.RPC("FinalizeRound", PhotonTargets.All, _gm.GetWinners().ToArray());
            }
        }
	}

    public void StartGame(List<PlayerData> players, int roundTime, int totalRounds)
    {
        var pauseMenu = Instantiate(Resources.Load<GameData>("GameData").PauseMenu);
        pauseMenu.GetComponent<PauseMenu>().OnExit = LeaveGame;
        _cleanUpList.Add(pauseMenu);

        _GameModeCollection = gameObject.AddComponent<GameModeCollection>();
        _GameModeCollection.AddCollection(Resources.Load<GameData>("GameData").GameModePrefabs);

        _LevelCollection = gameObject.AddComponent<LevelCollection>();
        _LevelCollection.AddCollection(Resources.Load<GameData>("GameData").Levels);

        _roundTime = roundTime;
        _totalRounds = totalRounds;
        foreach (var obj in players)
        {
            var playerObj = new GameObject();
            _cleanUpList.Add(playerObj);
            playerObj.name = obj.PlayerName;
            var player = playerObj.AddComponent<PlayerManager>();
            player.Initialise(obj);
            player.CreatePlayerCallback += OnPlayerCreated;
            Container<IPlayerManager>.Instance.Add(player);
        }
        _initialized = true;

        StartRound();

    }

    void StartRound()
    {
        if (!_initialized)
            return;

        CleanUp();
        _state = GameState.Gameplay;
        _currentRound++;

        

        if (!IsMaster)
            return;

        var level = _LevelCollection.GetRandomItem(11);
        var gameModeIndex = _GameModeCollection.GetRandomItemIndex(13);
        if (PhotonNetwork.offlineMode)
        {
            //PhotonNetwork.LoadLevel(level);
            SceneManager.LoadScene(level);
            StartCoroutine(FinalizeSetupRound(gameModeIndex, PhotonNetwork.AllocateViewID()));
        }
        else
        {
            var viewId = PhotonNetwork.AllocateViewID();
            photonView.RPC("SetupRound", PhotonTargets.All, level, gameModeIndex, viewId, PhotonNetwork.player);
        }
    }

    [PunRPC]
    void SetupRound(string sceneName, int gamemode, int viewId, PhotonPlayer player)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(FinalizeSetupRound(gamemode, viewId));
    }

    IEnumerator FinalizeSetupRound(int gamemode, int viewId)
    {
        yield return new WaitForSeconds(.5f);

        //Gamemode must be instantiated over network
        var obj = GameObject.Instantiate(_GameModeCollection.GetItem(gamemode));
        obj.name = "GameMode";
        var gameMode = obj.GetComponent<GameMode>();

        var pObj = GameObject.FindGameObjectWithTag("Playfield");
        var playfield = pObj.GetComponent<Playfield>();
        gameMode.Initialize(playfield);

        var view = gameMode.photonView;
        view.viewID = viewId;
        view.ownershipTransfer = OwnershipOption.Takeover;
        view.ObservedComponents.Add(gameMode);

        _gm = gameMode;
        _inGameUi = Instantiate(Resources.Load<GameData>("GameData").InGameUi).GetComponent<InGameUi>();

        //Must be done on all clients
        var players = Container<IPlayerManager>.Instance.Items;
        foreach (var player in players)
        {
            if (PhotonNetwork.offlineMode)
            {
                player.CreatePlayer(playfield.GetSpawnLocation(player.Index));
            }
            else if (player.Index == (int)PhotonNetwork.player.CustomProperties["Index"])
            {
                player.CreatePlayer(playfield.GetSpawnLocation(player.Index));
            }
            else if(PhotonNetwork.isMasterClient && player.Ai)
            {
                player.CreatePlayer(playfield.GetSpawnLocation(player.Index));
            }
        }

    }

    [PunRPC]
    void FinalizeRound(int[] winners)
    {
        _state = GameState.Scoreboard;
        Destroy(_inGameUi.gameObject);

        foreach (var player in Container<IPlayerManager>.Instance.Items)
        {
            if (winners.Contains(player.Index))
                player.RoundsWon++;
        }
        var playerScores = Container<IPlayerManager>.Instance.Items.ToDictionary(player => player.Index, player => player.RoundsWon);

        if (_currentRound < _totalRounds)
        {
            if (Resources.Load<GameData>("GameData").EndOfRoundMenu)
            {
                var obj = Instantiate(Resources.Load<GameData>("GameData").EndOfRoundMenu);
                var comp = obj.GetComponent<EndGameManager>();
                comp.AddPlayerScores(playerScores);
                comp.BackEndOfRound.onClick.AddListener(StartRound);
                if (!IsMaster)
                    comp.BackEndOfRound.enabled = false;
                CleanUp();
            }
            else if (IsMaster)
            {
                StartRound();
            }
        }
        else
        {
            if (Resources.Load<GameData>("GameData").EndOfRoundMenu)
            {
                var obj = Instantiate(Resources.Load<GameData>("GameData").EndOfRoundMenu);
                var comp = obj.GetComponent<EndGameManager>();
                comp.AddPlayerScores(playerScores);
                comp.BackEndOfRound.onClick.AddListener(LeaveGame);
                if (!IsMaster)
                    comp.BackEndOfRound.enabled = false;

                CleanUp();
            }
            else if(IsMaster)
            {
                photonView.RPC("EndGame", PhotonTargets.AllBuffered);
            }
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (PhotonNetwork.player.Equals(newMasterClient))
        {
            if (_state == GameState.Scoreboard)
            {
                var comp = FindObjectOfType<EndGameManager>();
                comp.BackEndOfRound.enabled = true;
            }
        }
    }

    void OnPlayerCreated(IPlayerManager player)
    {
        _inGameUi.AddPlayer(player);
    }

    //Inbetween rounds cleanup
    void CleanUp()
    {
        _gm = null;
        foreach (var player in Container<IPlayerManager>.Instance.Items)
        {
            player.DestroyPlayer();
        }
    }

    [PunRPC]
    void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        ContainerManager.CleanUp();
        Destroy(PickUpCollection.Instance.gameObject);
        foreach (var obj in _cleanUpList)
        {
            Destroy(obj);
        }
        Destroy(this.gameObject);
        SceneManager.LoadScene("Main"); //TODO don't hardcode
    }

    void OnDestroy()
    {
        Destroy(EventScript.Instance.gameObject);
    }
}
