using UnityEngine;
using Tribot;

[CreateAssetMenu(fileName = "AiData", menuName = "Data/Ai")]
public class AiData : ScriptableObject
{
    public Range RefreshRate;
    public float CloseRangeDistance = 1.5f;
    public float MoveSpeed = 6f;
    public float RotationSpeed = 500f;
    public Vector3 BoxSize = new Vector3(.5f, .5f, 1f);
    public Vector3 HeightOffset = new Vector3(0f, 1f, 0f);
}

