using UnityEngine;
using Tribot;
using System;

public class MenuState : TribotBehaviour, FsmState<Main.MenuStates>
{
    public float Speed = 1.5f;
    public RectTransform Rect;
    public Main.MenuStates State;

    public bool IsActive { get; set; }

    public FsmAction<Main.MenuStates> TransitionEvent { protected get; set; }
    public FsmAction<Main.MenuStates> PushEvent { protected get; set; }
    public Action PopEvent { protected get; set; }

    private Vector2 _target;

    void Start()
    {
    }

    public virtual void EnterState(Action EnterEvent = null)
    {
        CancelInvoke("DisableScreen");
        IsActive = true;
        gameObject.SetActive(true);

        Rect.offsetMin = Rect.offsetMax = Vector2.left*Screen.width;
        _target = Vector2.zero;
    }

    public virtual void ExitState(Action ExitEvent = null)
    {
        IsActive = false;
        _target = Vector3.right;
        Invoke("DisableScreen", Speed);
    }

    public virtual void InitState(Action InitEvent = null)
    {
        Rect.offsetMin = Rect.offsetMax = (_target = Vector2.right) * Screen.width;
        IsActive = false;
        gameObject.SetActive(false);
    }

    public virtual void UpdateState()
    {
        
    }

    void Update()
    {
        if (Rect.offsetMin != _target)
        {
            Rect.offsetMin = Vector2.MoveTowards(Rect.offsetMin, _target * Screen.width, Screen.width * Speed * Time.deltaTime);
            Rect.offsetMax = Vector2.MoveTowards(Rect.offsetMax, _target * Screen.width, Screen.width * Speed * Time.deltaTime);
        }

        if (!IsActive && Rect.offsetMin == _target * Screen.width)
        {
            DisableScreen();
        }
    }

    void DisableScreen()
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
}