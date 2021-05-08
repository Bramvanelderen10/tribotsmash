using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerData : ScriptableObject
{
    public enum PlayerType
    {
        Local,
        Online,
        Ai,
    }

    public int PlayerIndex = 0;
    public int ControlIndex = 0; //FOR LOCAL PLAY ONLY
    public PlayerType Type = PlayerType.Local;
    public string PlayerName = "Default";
    public CharacterIndex SelectedCharacter;
}
