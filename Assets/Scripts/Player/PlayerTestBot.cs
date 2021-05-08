using UnityEngine;
using Photon;
using System;
using Tribot;

/// <summary>
/// 
/// </summary>
public class PlayerTestBot : PunBehaviour, IPlayerManager
{

    private PlayerInfo _info;

    void Awake()
    {
        PhotonNetwork.offlineMode = true;
        if (!PhotonNetwork.inRoom)
            PhotonNetwork.JoinRoom("Local");


        Container<IPlayerManager>.Instance.Add(this);
    }

    void Start()
    {
        _info = GetComponent<PlayerInfo>();
    }

    public bool Ai
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public int Index
    {
        get { return _info.Index; }
    }

    public int InputIndex
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public bool IsAlive
    {
        get { return true; }
    }

    public bool IsEnabled
    {
        get { return true; }

        set
        {
            throw new NotImplementedException();
        }
    }

    public GameObject PlayerObject
    {
        get { return gameObject; }
    }

    public int RoundsWon
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public DelCreatePlayer CreatePlayerCallback
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public string Name
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public bool AddAbility(GameObject prefab)
    {
        throw new NotImplementedException();
    }

    public Component AddComponent(Type type)
    {
        throw new NotImplementedException();
    }

    public bool CreatePlayer(Vector3 pos, Vector3 rotation = default(Vector3))
    {
        throw new NotImplementedException();
    }

    public bool DestroyPlayer()
    {
        throw new NotImplementedException();
    }

    public bool SetPosition(Vector3 pos)
    {
        throw new NotImplementedException();
    }

    public bool SetRotation(Quaternion rot)
    {
        throw new NotImplementedException();
    }

    public bool AddAbility(AbilityTypes ability)
    {
        throw new NotImplementedException();
    }

    public void PopUp(string mainText, string subText)
    {
        throw new NotImplementedException();
    }
}


