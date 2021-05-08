using System;
using System.Collections.Generic;
using UnityEngine;
using Tribot;
using TMPro;
using UnityEngine.UI;

public class PlayerDialog : TribotBehaviour
{
    public SaveName Callback;
    public delegate void SaveName(string value);

    [SerializeField] private TMP_InputField _nameField;

    void Start()
    {
        EventScript.Instance.SetSelectedGameObject(_nameField.gameObject);
    }

    /// <summary>
    /// Sets the input field name to a new value
    /// </summary>
    public string Name
    {
        set { _nameField.text = value; }
    }

    /// <summary>
    /// Does a callback to whoever subscribed to the event then destroys itself
    /// </summary>
    public void Save()
    {
        if (Callback != null)
            Callback(_nameField.text);

        Destroy(gameObject);
    }
 }