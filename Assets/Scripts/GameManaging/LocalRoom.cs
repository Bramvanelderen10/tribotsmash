using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tribot;
using TMPro;
using System;

/// <summary>
/// This class handles the game creation UI.
/// It holds all the data that the user configures and then starts the game after the user is ready
/// </summary>
public class LocalRoom : MenuState
{
    //A list of texts displayed on the game creation screen, 1 for each player
    public List<TextMeshProUGUI> PlayerTexts = new List<TextMeshProUGUI>
    {
        null,
        null,
        null,
        null,
    };
    public string PlayerJoinText = "PRESS START"; //The text values used in playertext
    public TextMeshProUGUI RoundTime;
    public TextMeshProUGUI TotalRounds;
    public CharacterSelectManager _characterSelect;

    private List<PlayerMap> _players;


    // Use this for initialization
    public override void InitState(Action InitEvent = null)
    {
        base.InitState(InitEvent);
        _characterSelect.enabled = false;
        _characterSelect.IterateSkinCallback = Iterate;
        foreach (var text in PlayerTexts)
	    {
	        text.text = PlayerJoinText;
	    }
	}

    public override void EnterState(Action EnterEvent = null)
    {
        base.EnterState(EnterEvent);
        _characterSelect.enabled = true;

        foreach (var text in PlayerTexts.Where(text => text))
        {
            text.text = PlayerJoinText;
        }

        _players = new List<PlayerMap>
        {
            null,
            null,
            null,
            null,
        };
    }

    public override void ExitState(Action ExitEvent = null)
    {
        base.ExitState(ExitEvent);
        _characterSelect.enabled = false;
    }

    // Update is called once per frame
    public override void UpdateState()
    {
        base.UpdateState();

	    for (var i = 0; i < 5; i ++)
	    {
	        if (TribotInput.GetButtonDown(TribotInput.InputButtons.Start, i))
	        {
	            bool exists = false;
	            foreach (var map in _players.Where(map => map != null && map.GamepadIndex == (TribotInput.Index) i + 1))
	                exists = true;

	            if (!exists)
	            {
	                for (int j = 0; j < _players.Count; j++)
	                {
	                    if (_players[j] == null)
	                    {
	                        _players[j] = new PlayerMap() {GamepadIndex = (TribotInput.Index) i + 1};
                            _characterSelect.EnableButton(j, false);
                            break;
                        }
	                    
	                }
	            }
            }

            if (TribotInput.GetButtonDown(TribotInput.InputButtons.Back, i))
            {
                bool exists = false;
                foreach (var map in _players.Where(map => map != null && map.GamepadIndex == (TribotInput.Index)i + 1))
                    exists = true;

                if (exists)
                {
                    for (int j = 0; j < _players.Count; j++)
                    {
                        if (_players[j] != null)
                        {
                            _players[j] = null;
                            _characterSelect.DisableButton(j);
                            break;
                        }

                    }
                }
            }
        }

	    for (var i = 0; i < _players.Count; i++)
        {
            if (_players[i] != null)
            {
                if (PlayerTexts[i])
                    PlayerTexts[i].text = "Player " + (i + 1);

                if (TribotInput.GetButtonDown(TribotInput.InputButtons.RT, _players[i].Index))
                {
                    Iterate(IterateType.Next, ModelSkin.Model,
                        i);
                }
                if (TribotInput.GetButtonDown(TribotInput.InputButtons.RB, _players[i].Index))
                {
                    Iterate(IterateType.Next, ModelSkin.Skin,
                        i);
                }

                if (TribotInput.GetButtonDown(TribotInput.InputButtons.LT, _players[i].Index))
                {
                    Iterate(IterateType.Previous, ModelSkin.Model,
                        i);

                }
                if (TribotInput.GetButtonDown(TribotInput.InputButtons.LB, _players[i].Index))
                {
                    Iterate(IterateType.Previous, ModelSkin.Skin, i);
                }
            }
        }
	}
    
    void Iterate(IterateType iType, ModelSkin type, int index)
    {
        _characterSelect.Iterate(iType, type, index);
    }

    public void StartGame()
    {
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.JoinRoom("Local"); //IMPORTANT TO JOIN ROOM SO RPC CALLS WORK LOCALLY
        PhotonNetwork.room.IsOpen = false;

        var obj = new GameObject {name = "GameManager"};
        obj.AddComponent<PhotonView>().viewID = PhotonNetwork.AllocateViewID();
        var gameManager = obj.AddComponent<GameManager>();

        int roundTime;
        if (!int.TryParse(RoundTime.text, out roundTime))
            roundTime = 120;

        int totalRounds;
        if (!int.TryParse(TotalRounds.text, out totalRounds))
            totalRounds = 20;

        var players = new List<PlayerData>();
        for (int i = 0; i < 4; i++)
        {
            var data = ScriptableObject.CreateInstance<PlayerData>();
            data.PlayerIndex = i;
            data.ControlIndex = 0;
            data.SelectedCharacter = _characterSelect.SelectedCharacters[i];
            if (_players[i] != null)
            {
                data.ControlIndex = _players[i].Index;
                data.Type = PlayerData.PlayerType.Local;
                data.PlayerName = "Player " + (i + 1);
            }
            else
            {
                data.Type = PlayerData.PlayerType.Ai;
                data.PlayerName = PlayerSettings.Instance.RandomName;
            }
            players.Add(data);
        }
        
        gameManager.StartGame(players, roundTime, totalRounds);
    }

    public void Back()
    {

        PopEvent();
    }
}

//TODO UGLY CODE MAKE NICER PLS
public class PlayerMap
{
    public TribotInput.Index GamepadIndex = TribotInput.Index.Any;
    public int Index { get { return (int) (GamepadIndex - 1); } }
    public bool isPressedRT = false;
    public bool isPressedLT = false;
}
