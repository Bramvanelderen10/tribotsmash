using System.Collections.Generic;
using UnityEngine;
using Tribot;
using TMPro;

/// <summary>
/// Manager of manager of managers super god bla bla
/// </summary>
public class Main : TribotBehaviour
{
    public enum MenuStates
    {
        Main,
        LocalRoom,
        MultiplayerRoom,
        Settings,
    }

    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private List<MenuState> _menus;
    private PlayerSettings _pSettings;

    private FsmSystem<MenuStates> _fsm;

    void Start()
    {
        _fsm = FsmSystem<MenuStates>.Initialize(this);
        foreach (var obj in _menus)
            _fsm.AddState(obj, obj.State);
        _fsm.Push(MenuStates.Main);

        PlayerSettings.Instance.OnNameChange = UpdateName;
        UpdateName();
    }

    void Update()
    {
        
    }

    void UpdateName()
    {
        _playerName.text = PlayerSettings.Instance.PlayerName;
    }

    public void Transition(int state)
    {
        _fsm.Push((MenuStates)state);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenPlayerNameDialog()
    {
        PlayerSettings.Instance.OpenDialog();
    }
}