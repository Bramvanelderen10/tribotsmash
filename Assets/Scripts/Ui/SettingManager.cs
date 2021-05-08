using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
using System;
using Tribot;

/// <summary>
/// Shows edits and applies settings
/// </summary
public class SettingManager : MenuState
{
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _master = "MasterVolume";
    [SerializeField] private string _music = "MusicVolume";
    [SerializeField] private string _sfx = "SfxVolume";
    [Space]
    [Header("Ui Dependencies")]
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Button _resolution;
    [SerializeField] private Button _resolutionLeft;
    [SerializeField] private Button _resolutionRight;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    private Resolution[] _resolutions;
    private int _currentRes = 0;

    private float _lastAxisValue = 0;
    private GameSettings _gameSettings;

    public Action Callback;

    public override void InitState(Action InitEvent = null)
    {
        base.InitState(InitEvent);
    }

    public override void EnterState(Action EnterEvent = null)
    {
        base.EnterState(EnterEvent);
        _resolutions = Screen.resolutions;
        _gameSettings = new GameSettings();
        _currentRes = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);
        SaveSettings();
        LoadAndApplySettings();

        _resolutionLeft.onClick.AddListener(OnResLeft);
        _resolutionRight.onClick.AddListener(OnResRight);
    }

    public override void UpdateState()
    {
        var h = Input.GetAxis("UIHorizontal");
        if (EventScript.Instance.currentSelectedGameObject == _resolution.gameObject)
        {
            if (h > 0f && _lastAxisValue == 0f)
            {
                OnResRight();

            }
            else if (h < 0f && _lastAxisValue == 0f)
            {
                OnResLeft();
            }
        }
        _lastAxisValue = h;
    }

    public override void ExitState(Action ExitEvent = null)
    {
        base.ExitState(ExitEvent);
        LoadAndApplySettings();
    }

    public void Back()
    {
        if (IsActive)
        {
            PopEvent();
        }
    }

    public void SaveAndApplySettings()
    {
        if (IsActive)
        {
            SaveSettings();
            _gameSettings.SaveSettingsToFile();
            PopEvent();
        }
    }

    /// <summary>
    /// SavesSettings to the gamesettings class
    /// </summary>
    void SaveSettings()
    {
        _gameSettings.Fullscreen = _fullscreenToggle.isOn;
        _gameSettings.ResolutionIndex = _currentRes;
        _gameSettings.Vsync = _vsyncToggle.isOn;
        _gameSettings.MasterVolume = _masterVolumeSlider.value;
        _gameSettings.SfxVolume = _sfxVolumeSlider.value;
        _gameSettings.MusicVolume = _musicVolumeSlider.value;
    }  

    /// <summary>
    /// Loads settings from file and applies them to the game
    /// </summary>
    void LoadAndApplySettings()
    {
        _gameSettings.LoadSettingsFromFile();
        Screen.fullScreen = _gameSettings.Fullscreen;
        _fullscreenToggle.isOn = _gameSettings.Fullscreen;

        _currentRes = _gameSettings.ResolutionIndex;
        if (_currentRes >= _resolutions.Length)
        {
            _currentRes = _resolutions.Length - 1;
            _gameSettings.ResolutionIndex = _currentRes;
            _gameSettings.SaveSettingsToFile();
            LoadAndApplySettings();
            return;
        }
        var res = _resolutions[_currentRes];
        _resolution.transform.parent.GetComponent<TextMeshProUGUI>().text = res.width + "x" + res.height + " " + res.refreshRate + "Hz";
        Screen.SetResolution(res.width, res.height, _gameSettings.Fullscreen, res.refreshRate);

        QualitySettings.vSyncCount = (_gameSettings.Vsync) ? 2 : 0;
        _vsyncToggle.isOn = _gameSettings.Vsync;

        _audioMixer.SetFloat(_master, _gameSettings.MasterVolume);
        _masterVolumeSlider.value = _gameSettings.MasterVolume;

        _audioMixer.SetFloat(_music, _gameSettings.MusicVolume);
        _musicVolumeSlider.value = _gameSettings.MusicVolume;

        _audioMixer.SetFloat(_sfx, _gameSettings.SfxVolume);
        _sfxVolumeSlider.value = _gameSettings.SfxVolume;
    }

    /// <summary>
    /// Moves resolution index down
    /// </summary>
    public void OnResLeft()
    {
        _currentRes--;
        if (_currentRes < 0)
        {
            _currentRes = _resolutions.Length - 1;
        }
        OnResolutionChange();
    }

    /// <summary>
    /// Moves resolution index up
    /// </summary>
    public void OnResRight()
    {
        _currentRes++;
        if (_currentRes > _resolutions.Length - 1)
        {
            _currentRes = 0;
        }
        OnResolutionChange();
    }

    /// <summary>
    /// Updated resolution UI
    /// </summary>
    public void OnResolutionChange()
    {
        var res = _resolutions[_currentRes];
        _resolution.transform.parent.GetComponent<TextMeshProUGUI>().text = res.width + "x" + res.height + " " + res.refreshRate + "Hz";
        _gameSettings.ResolutionIndex = _currentRes;
    }
}
