using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "Data/Pick Up Data")]
public class PickUpData : ScriptableObject
{
    [Header("Settings")]
    public float StartHeightOffset = 5f;
    public float IdleSpeed = 5f;
    public float DropSpeed = 2f;
    public float CurveHeight = .1f;
    public float LifeTime = 10f;
    [Header("Prefabs")]
    public List<GameObject> Prefabs = new List<GameObject>();

}
