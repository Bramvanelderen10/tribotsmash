using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Holds game settings and loads/saves them to file
/// </summary>
[Serializable]
public class GameSettings
{
    public string FileName = "GameSettings.json";
    public bool Fullscreen = false;
    public int ResolutionIndex = 0;
    public bool Vsync = false;
    public float MasterVolume = 1;
    public float SfxVolume = 1;
    public float MusicVolume = 1;

    /// <summary>
    /// Loads settings from file, if it doesn't exist write a file with default values then reloads them
    /// </summary>
    public void LoadSettingsFromFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + FileName))
        {
            File.WriteAllText(Application.persistentDataPath + "/" + FileName, JsonUtility.ToJson(this));
        }
        var temp = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/" + FileName));
        FileName = temp.FileName;
        Fullscreen = temp.Fullscreen;
        ResolutionIndex = temp.ResolutionIndex;
        Vsync = temp.Vsync;
        MasterVolume = temp.MasterVolume;
        SfxVolume = temp.SfxVolume;
        MusicVolume = temp.MusicVolume;
    }

    /// <summary>
    /// Writes current settings to file
    /// </summary>
    public void SaveSettingsToFile()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + FileName, JsonUtility.ToJson(this));
    }
}
