using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IPlayerManager
{
    //Properties
    bool IsEnabled { get; set; }
    bool IsAlive { get; }
    bool Ai { get; set; }
    int RoundsWon { get; set; }
    int Index { get; }
    int InputIndex { get; set; }
    GameObject PlayerObject { get; }
    DelCreatePlayer CreatePlayerCallback { get; set; }
    string Name { get; }

    //Methods
    bool SetPosition(Vector3 pos);
    bool SetRotation(Quaternion rot);
    bool CreatePlayer(Vector3 pos, Vector3 rotation = default(Vector3));
    bool DestroyPlayer();
    Component AddComponent(Type type);
    bool AddAbility(AbilityTypes ability);
    void PopUp(string mainText, string subText);
}

public delegate void DelCreatePlayer(IPlayerManager player);

