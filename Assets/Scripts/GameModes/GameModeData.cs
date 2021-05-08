using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Game Mode Data")]
public class GameModeData : ScriptableObject
{
    [Header("Settings")]
    public float PreGameDuration = 3f;
    public LayerMask GroundLayer;
    public LayerMask BorderLayer;

    [Header("Prefabs")]
    public GameObject InGameUi;
    public GameObject GameModeIntruction;
}
