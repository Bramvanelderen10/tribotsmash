using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class PlayerWindow : EditorWindow {
    private string _name = "Player";
    private Vector3 _position = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private bool _testPlayer = false;
    private bool _ai = false;
    private bool _hp = false;

    private int _index = 0;
    private bool _autoIndex = true;

    private bool _showAbilities = false;
    private List<AbilityMap> _abilities;

    private AbilityData _data;

    [MenuItem("Tribot/Create Player")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerWindow));
    }

    void OnEnable()
    {
        LoadAbilities();
    }

    void OnGUI()
    {
        // The actual window code goes here
        titleContent = new GUIContent("Create Player");
        GUILayout.Label("Player Settings", EditorStyles.boldLabel);

        GUILayout.Space(8);

        //Naming area
        GUILayout.BeginHorizontal();
        GUILayout.Label("Player Name");
        _name = EditorGUILayout.TextField(_name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Auto Index");
        _autoIndex = EditorGUILayout.Toggle(_autoIndex);
        GUILayout.EndHorizontal();
        if (!_autoIndex)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Player Index");
            _index = EditorGUILayout.IntField(_index);
            GUILayout.EndHorizontal();
        }

        _position = EditorGUILayout.Vector3Field("Position", _position);
        _rotation = EditorGUILayout.Vector3Field("Rotation", _rotation);

        GUILayout.Space(8);


        _showAbilities = EditorGUILayout.Foldout(_showAbilities, "Abilities", EditorStyles.boldFont);
        if (_showAbilities)
        {
            GUILayout.BeginVertical();
            foreach (var ability in _abilities)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(ability.Ability.ToString());
                ability.Selected = EditorGUILayout.Toggle(ability.Selected);
                GUILayout.EndHorizontal();

            }
            GUILayout.EndVertical();
        }

        GUILayout.Space(8);


        GUILayout.BeginHorizontal();
        GUILayout.Label("Ai Player");
        _ai = EditorGUILayout.Toggle(_ai);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Test Player");
        _testPlayer = EditorGUILayout.Toggle(_testPlayer);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Add HP");
        _hp = EditorGUILayout.Toggle(_hp);
        GUILayout.EndHorizontal();

        if (GUILayout.Button(new GUIContent("Create Player")))
        {
            //Do creation stuff
            CreatePlayer();
            Clear();
        }
    }

    void Clear()
    {
        _name = "Player";
        _position = Vector3.zero;
        _rotation = Vector3.zero;
        _testPlayer = false;

        Close();
    }

    void CreatePlayer()
    {
        var cd = Resources.Load<CharacterData>("CharacterData");
        var index = (_autoIndex) ? FindObjectsOfType<PlayerInfo>().Length : _index;
        var player = cd.InstatiateCharacter(new CharacterIndex() { ModelIndex = 0, SkinIndex = 0 });
        player.transform.position = _position;
        player.transform.rotation = Quaternion.Euler(_rotation);
        player.name = _name;

        var info = player.GetComponent<PlayerInfo>();
        info.Index = index;

        var playerComp = player.GetComponent<Player>();

        foreach (var ability in _abilities)
        {
            
            if (ability.Selected)
            {
                playerComp.TESTINGONLYStarterAbilities.Add(ability.Ability);
            }
        }

        if (_testPlayer)
        {
            player.AddComponent<PlayerTestBot>();
            if (_ai)
            {
                var controlled = player.AddComponent<AiControlled>();

            }
            else
            {
                var controlled = player.AddComponent<PlayerControlled>();
                controlled.InputIndex = index;
            }
        }

        if (_hp)
        {
            var hp = player.AddComponent<PlayerHitpoints>();
            hp.Hitpoints = 1;
            hp.DisplayHpBar = false;
        }

        playerComp.photonView.viewID = PhotonNetwork.AllocateViewID();
    }

    void LoadAbilities()
    {
        _abilities = new List<AbilityMap>();
        int index = 0;
        _data = Resources.Load<AbilityData>("AbilityData");
        foreach (var item in _data._abilities)
        {
            _abilities.Add(new AbilityMap(index, item.Ability));
            index++;
        }
    }

    class AbilityMap
    {
        public int Index;
        public AbilityTypes Ability;
        public bool Selected = false;

        public AbilityMap(int index, AbilityTypes ability)
        {
            Index = index;
            Ability = ability;
        }
    }
}