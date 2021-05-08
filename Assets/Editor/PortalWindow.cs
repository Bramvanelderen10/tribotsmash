using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// 
/// </summary>
public class PortalWindow : EditorWindow
{
    [SerializeField] private List<PortalMap> _portals = new List<PortalMap>();

    private string _portalName = "Portal";
    private Vector3 _portalPosition = Vector3.zero;
    private Vector3 _portalRotation = Vector3.zero;
    private static bool _showConnectedPortals = false;

    [MenuItem("Tribot/Create Portal")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PortalWindow));
    }

    void OnEnable()
    {
        RetrieveExistingPortals();
    }

    void OnGUI()
    {
        // The actual window code goes here
        titleContent = new GUIContent("Create Portal");
        GUILayout.Label("Portal Settings", EditorStyles.boldLabel);

        GUILayout.Space(8);

        //Naming area
        GUILayout.BeginHorizontal();
        GUILayout.Label("PortalName");
        _portalName = EditorGUILayout.TextField(_portalName);
        GUILayout.EndHorizontal();

        _portalPosition = EditorGUILayout.Vector3Field("Position", _portalPosition);
        _portalRotation = EditorGUILayout.Vector3Field("Rotation", _portalRotation);

        GUILayout.Space(8);

        _showConnectedPortals = EditorGUILayout.Foldout(_showConnectedPortals, "Connected Portals", EditorStyles.boldFont);
        if (_showConnectedPortals)
        {
            GUILayout.BeginVertical();
            foreach (var portal in _portals)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(portal.Portal.gameObject.name);
                portal.Selected = EditorGUILayout.Toggle(portal.Selected);
                GUILayout.EndHorizontal();

            }
            GUILayout.EndVertical();
        }

        if (GUILayout.Button(new GUIContent("Create Portal")))
        {
            //Do creation stuff
            CreatePortal();
            Clear();
        }
    }

    void Clear()
    {
        _portals = new List<PortalMap>();
        _showConnectedPortals = false;
        _portalName = "Portal";
        _portalPosition = Vector3.zero;
        _portalRotation = Vector3.zero;

        Close();
    }

    void CreatePortal()
    {
        var guids = AssetDatabase.FindAssets("l:Portal");
        GameObject portalPrefab = null;
        foreach (var guid in guids)
        {
            portalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
        }

        var portal = Instantiate(portalPrefab, _portalPosition, Quaternion.Euler(_portalRotation));
        portal.name = _portalName;
        var comp = portal.GetComponent<PortalController>();
        foreach (var item in _portals)
        {
            if (item.Selected = true)
            {
                comp.AddConnectedPortal(item.Portal);
            }
        }

    }

    void RetrieveExistingPortals()
    {
        int index = 0;
        foreach (var item in GameObject.FindObjectsOfType<PortalController>())
        {
            _portals.Add(new PortalMap(index, item));

            index++;
        }
    }

    class PortalMap
    {
        public int Index;
        public PortalController Portal;
        public bool Selected = false;

        public PortalMap(int index, PortalController portal)
        {
            Index = index;
            Portal = portal;
        }
    }
}