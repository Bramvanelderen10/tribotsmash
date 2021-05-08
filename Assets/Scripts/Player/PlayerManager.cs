using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Tribot;

/// <summary>
/// Managed the player outside the rounds.
/// This class can create and clear the in game player, assign AI or human input.
/// Other managers commmunicate with the player through the playermanager.
/// </summary>
public class PlayerManager : Tribot.TribotBehaviour, IPlayerManager
{
    public bool IsEnabled { get; set; }

    public bool IsAlive
    {
        get { return PlayerComponent.IsAlive; }
    }

    public bool Ai { get; set; }
    public int Index { get; private set; }
    public int RoundsWon { get; set; }
    public Player PlayerComponent { get; private set; }
    public int InputIndex
    {
        get { return _data.ControlIndex; }
        set { _data.ControlIndex = value; }
    }
    public GameObject PlayerObject { get { return _player; } }
    public DelCreatePlayer CreatePlayerCallback { get; set; }

    public string Name
    {
        get { return _data.PlayerName; }
    }

    private GameObject _player;
    private PlayerData _data;
    private CharacterData _charData;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Initialise(PlayerData data)
    {
        _charData = Resources.Load<CharacterData>("CharacterData");
        _data = data;
        Index = _data.PlayerIndex;
        Ai = (_data.Type == PlayerData.PlayerType.Ai);
        RoundsWon = 0;
        IsEnabled = false;
        var view = gameObject.AddComponent<PhotonView>();
        view.viewID = 20 + Index;
        view.ObservedComponents = new List<Component> {this};
    }

    public bool CreatePlayer(Vector3 pos, Vector3 rotation = default(Vector3))
    {
        var viewId = PhotonNetwork.AllocateViewID();
        var position = new Vector3(pos.x, pos.y + 1f, pos.z);
        var rot = Quaternion.Euler(rotation);

        if (PhotonNetwork.offlineMode)
        {
            if (IsEnabled)
            {
                if (Ai)
                {
                    if (_player.GetComponent<Player>().GetType() == typeof(PlayerControlled))
                    {
                        position = _player.transform.position;
                        rot = _player.transform.rotation;
                        DestroyPlayer();
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (_player.GetComponent<Player>().GetType() == typeof(AiControlled))
                    {
                        position = _player.transform.position;
                        rot = _player.transform.rotation;
                        DestroyPlayer();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            RpcCreatePlayer(viewId, Index, Ai, position, rot);
            AddController(_player);
        }
        else
        {
            RpcCreatePlayer(viewId, Index, Ai, position, rot);
            AddController(_player);
            photonView.RPC("RpcCreatePlayer", PhotonTargets.Others, viewId, Index, Ai, position, rot);
        }

        
        return true;
    }

    void AddController(GameObject player)
    {
        if (Ai)
        {
            player.AddComponent<AiControlled>();
        }
        else
        {
            var temp = player.AddComponent<PlayerControlled>();
            temp.InputIndex = InputIndex;
        }
    }

    [PunRPC]
    public void RpcCreatePlayer(int viewID, int index, bool ai, Vector3 position, Quaternion rotation)
    {
        _player = _charData.InstatiateCharacter(_data.SelectedCharacter);
        _player.transform.position = position;
        _player.transform.rotation = rotation;
        _player.name = "Player" + Index;
        _player.GetComponent<PlayerInfo>().Index = index;
        _player.GetComponent<PlayerInfo>().Ai = ai;
        _player.GetComponent<PlayerTextContainer>().SetName(_data.PlayerName);

        Ai = ai;
        PlayerComponent = _player.GetComponent<Player>();
        PlayerComponent.photonView.viewID = viewID;

        StartCoroutine(Callback());
        IsEnabled = true;
    }

    IEnumerator Callback()
    {
        yield return null;
        if (CreatePlayerCallback != null)
            CreatePlayerCallback(this);
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (Ai)
        {
            if (newMasterClient == PhotonNetwork.player)
            {
                AddController(PlayerObject);
            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        var index = (int)otherPlayer.CustomProperties["Index"];
        if (index == Index)
        {

            //ar pos = PlayerObject.transform.position;
            //var rot = PlayerObject.transform.rotation.eulerAngles;
            //DestroyPlayer();
            Ai = true;
            if (IsMaster)
            {
                _player.GetComponent<PlayerInfo>().Ai = Ai;
                _data.PlayerName = PlayerSettings.Instance.RandomName;
                _player.GetComponent<PlayerTextContainer>().SetName(_data.PlayerName);

                AddController(_player);
            }
        }
    }

    public bool DestroyPlayer()
    {
        if (!IsEnabled)
            return false;

        GameObject obj = _player;
        _player = null;
        PlayerComponent = null;
        GameObject.Destroy(obj);

        IsEnabled = false;

        return true;
    }

    public int GetIndex()
    {

        return (int) Index;
    }

    public bool SetPosition(Vector3 pos)
    {
        _player.transform.position = pos;

        return true;
    }

    public bool SetRotation(Quaternion rot)
    {
        _player.transform.rotation = rot;

        return true;
    }

    public Component AddComponent(Type type)
    {
        return !IsEnabled ? null : _player.AddComponent(type);
    }

    public bool AddAbility(AbilityTypes ability)
    {
        if (IsEnabled)
        {
            PlayerComponent.AddAbility(ability);
        }

        return true;
    }

    public void PopUp(string mainText, string subText)
    {
        if (!_player)
            return;

        _player.GetComponent<PlayerTextContainer>().Popup(mainText, subText);
    }
}

