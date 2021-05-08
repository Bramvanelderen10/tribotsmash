using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [Header("Prefabs")]
    public List<GameObject> GameModePrefabs;
    public GameObject PauseMenu;
    public GameObject EndOfRoundMenu;
    public GameObject InGameUi;
    public List<string> Levels;
    public string PlayerRoot;
}
