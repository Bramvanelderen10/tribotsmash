using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameModes", menuName = "Data/GameModes")]
public class GameModes : ScriptableObject
{
    public List<GameObject> GameModePrefabs;
}

