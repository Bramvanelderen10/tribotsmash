using UnityEngine;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Tribot;
using TMPro;

/// <summary>
/// 
/// </summary>
public class MpRoom : MenuState
{
    [SerializeField] private static string GameVersion = ".1";
    [SerializeField] private List<GameObject> _hostObjects = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI[] _playerNames = new TextMeshProUGUI[4];
    [SerializeField] private List<TextMeshProUGUI> _syncTexts = new List<TextMeshProUGUI>();
    [SerializeField] private CharacterSelectManager _characterSelect;

    private bool isPressedRT = false;
    private bool isPressedLT = false;

    void Awake()
    {
        SetHostObjects(false);
    }

    public override void InitState(Action InitEvent = null)
    {
        base.InitState(InitEvent);

        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.autoJoinLobby = true;
            PhotonNetwork.automaticallySyncScene = false;
            PhotonNetwork.autoCleanUpPlayerObjects = false;
        }

        _characterSelect.enabled = false;
        _characterSelect.IterateSkinCallback = IterateCharacter;
    }

    public override void EnterState(Action EnterEvent = null)
    {
        base.EnterState(EnterEvent);
        PhotonNetwork.offlineMode = false;
        StartCoroutine(StartConnection());
    }

    public override void ExitState(Action ExitEvent = null)
    {
        base.ExitState(ExitEvent);
        PhotonNetwork.Disconnect();
        SetHostObjects(false);
        _characterSelect.enabled = false;
    }

    public override void UpdateState()
    {
        if (isPressedRT)
        {
            if (TribotInput.GetButtonUp(TribotInput.InputButtons.RT, -1))
                isPressedRT = false;
        }
        if (isPressedLT)
        {
            if (TribotInput.GetButtonUp(TribotInput.InputButtons.LT, -1))
                isPressedLT = false;
        }

        if (PhotonNetwork.inRoom)
        {
            if (TribotInput.GetButton(TribotInput.InputButtons.RT, -1) && !isPressedRT)
            {
                isPressedRT = true;
                photonView.RPC("Iterate", PhotonTargets.MasterClient, IterateType.Next, ModelSkin.Model,
                    (int) PhotonNetwork.player.CustomProperties["Index"]);
            }
            if (TribotInput.GetButtonDown(TribotInput.InputButtons.RB, -1))
            {
                photonView.RPC("Iterate", PhotonTargets.MasterClient, IterateType.Next, ModelSkin.Skin,
                    (int)PhotonNetwork.player.CustomProperties["Index"]);
            }

            if (TribotInput.GetButton(TribotInput.InputButtons.LT, -1) && !isPressedLT)
            {
                isPressedLT = true;
                photonView.RPC("Iterate", PhotonTargets.MasterClient, IterateType.Previous, ModelSkin.Model,
                    (int)PhotonNetwork.player.CustomProperties["Index"]);

            }
            if (TribotInput.GetButtonDown(TribotInput.InputButtons.LB, -1))
            {
                photonView.RPC("Iterate", PhotonTargets.MasterClient, IterateType.Previous, ModelSkin.Skin,
                    (int)PhotonNetwork.player.CustomProperties["Index"]);
            }
        }
    }

    void IterateCharacter(IterateType iType, ModelSkin mType, int index)
    {
        photonView.RPC("Iterate", PhotonTargets.MasterClient, iType, mType, index);
    }

    [PunRPC]
    void Iterate(IterateType iType, ModelSkin type, int index)
    {
        _characterSelect.Iterate(iType, type, index);
    }

    public void Back()
    {
        PopEvent();
    }

    public void OnRoundChange()
    {
        
    }

    public void OnTimeChange()
    {

    }

    IEnumerator StartConnection()
    {
        ClearGUI();
        ConnectToMaster();
        yield return new WaitUntil(() => PhotonNetwork.connectedAndReady);
        _characterSelect.enabled = true;
        PhotonNetwork.JoinRandomRoom();

    }

    void CreateRoom()
    {
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        SetHostObjects(true);
    }

    void ConnectToMaster()
    {
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings(GameVersion);
    }

    public override void OnCreatedRoom()
    {
        SetHostObjects(true);
        Utilities.SetCustomProperties(PhotonNetwork.player, PhotonNetwork.playerList.Length - 1);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Utilities.SetCustomProperties(newPlayer, PhotonNetwork.playerList.Length - 1);
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            int playerIndex = 0;
            foreach (var p in PhotonNetwork.playerList)
                Utilities.SetCustomProperties(p, playerIndex++);
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (PhotonNetwork.player.Equals(newMasterClient))
        {
            SetHostObjects(true);
        }
    }

    public override void OnJoinedRoom()
    {
        
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        CreateRoom();
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        UpdatePlayerList();
    }

    void OnPhotonSerializeView(
        PhotonStream stream,
        PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
            foreach (var text in _syncTexts)
            {
                stream.SendNext(text.text);
            }
        }
        else
        {
            foreach (var text in _syncTexts)
            {
                text.text = (string) stream.ReceiveNext();
            }
        }
    }
    
    public void UpdatePlayerList()
    {
        ClearGUI();

        int playerIndex = 0;
        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            //Update names
            var name = _playerNames[playerIndex++];
            if (name)
                name.text = p.NickName;

            // Do local player stuff
            if (p == PhotonNetwork.player)
            {
                
            }

            /*
            // updates robot
            if (p.customProperties.ContainsKey("robot"))
            {
                
            }
            */
        }

        _characterSelect.EnableButton((int)PhotonNetwork.player.CustomProperties["Index"]);
    }

    void SetHostObjects(bool active)
    {
        foreach (var obj in _hostObjects)
            obj.SetActive(active);
    }

    void ClearGUI()
    {
        for (int i = 0; i < _playerNames.Length; i++)
        {
            _playerNames[i].text = PlayerSettings.Instance.RandomName;
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.IsOpen = false;
            photonView.RPC("StartGameSync", PhotonTargets.All, PhotonNetwork.AllocateViewID());
        }
    }

    [PunRPC]
    public void StartGameSync(int viewId)
    {
        var obj = new GameObject { name = "GameManager" };
        var gameManager = obj.AddComponent<GameManager>();
        
        var view = obj.AddComponent<PhotonView>();
        view.viewID = viewId;
        view.ownershipTransfer = OwnershipOption.Takeover;

        var players = new List<PlayerData>();
        for (int i = 0; i < 4; i++)
        {
            var data = ScriptableObject.CreateInstance<PlayerData>();
            data.PlayerIndex = i;
            data.SelectedCharacter = _characterSelect.SelectedCharacters[i];
            data.PlayerName = _playerNames[i].text;
            if (i < PhotonNetwork.playerList.Length)
            {
                data.Type = PlayerData.PlayerType.Online;
            }
            else
            {
                data.Type = PlayerData.PlayerType.Ai;
                
            }
            players.Add(data);
        }



        int roundTime;
        if (!int.TryParse(_syncTexts[1].text, out roundTime))
            roundTime = 120;

        int totalRounds;
        if (!int.TryParse(_syncTexts[0].text, out totalRounds))
            totalRounds = 20;

        gameManager.StartGame(players, roundTime, totalRounds);
    }
}
