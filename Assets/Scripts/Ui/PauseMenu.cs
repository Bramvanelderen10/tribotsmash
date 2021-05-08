using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tribot;

public class PauseMenu : MonoBehaviour
{
    public Action OnExit { get; set; }

    [SerializeField] private bool _pauseTime = false;
    [SerializeField] private GameObject _firstSelected;
    [SerializeField] private GameObject _canvas;

    private bool isActive = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Close();
    }

    void Update()
    {
        if (TribotInput.GetButtonDown(TribotInput.InputButtons.Start, -1))
        {
            if (isActive)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Open()
    {
        EventScript.Instance.SetSelectedGameObject(_firstSelected);
        _canvas.SetActive(true);
        isActive = true;
    }

    public void Close()
    {
        _canvas.SetActive(false);
        isActive = false;
    }

    public void Exit()
    {
        OnExit();
    }

}
