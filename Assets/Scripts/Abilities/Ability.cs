using UnityEngine;
using Tribot;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The class all abilities derive from.
/// By default an ability is executed in 3 steps
/// First the cast, here the animation event gets triggered and other optional logic can be added in the prepare method
/// Second the ability will execute, here you can trigger damage, visual fx sound fx and much more
/// Lastly the post cast happends, this is here to let any animation finish
/// </summary>
public abstract class Ability : TribotBehaviour
{
    [HideInInspector]
    public int PlayerIndex = 0;

    //Variables
    [Header("Generic Ability Settings")]
    public TribotInput.InputButtons MappedButton = TribotInput.InputButtons.A;
    public string AbilityName = "Ability";
    public Sprite UiIcon;
    [HideInInspector] public PlayerStateMachine PlayerState;
    public float CastMSMultiplier = 0.2f; //Is used by Player to determine how much slowdown in movement there is based on the ability
    public float Cooldown = 10f;
    public bool IsCasting = false; //IsCasting is used by player.cs to see if the ability is done casting
    public float PreCastTime = 0f;
    public float PostCastTime = 0f;
    public CastEvent CastCallBack;
    

    protected virtual bool CanInterrupt { get { return true; } }

    protected Coroutine _cast = null;
    protected float Timestamp = 0f;
    protected Player _player;

    //Properties
    //Abilities that have hold key functionality use this bool to determine if it needs to release
    public bool Release
    {
        protected get;
        set;
    }

    //Check if ability is off cooldown, can be overwritten by child if necesary
    public virtual bool IsReady
    {
        get { return Timestamp <= Time.time; }
    }

    //Determine current cooldown based on percentage, usefull for UI
    public float GetCooldown
    {
        get
        {
            if (IsReady)
                return 0;

            return (Timestamp - Time.time) / Cooldown;
        }
    }

    protected virtual void Start()
    {
        _player = transform.parent.GetComponent<Player>();
    }

    //Prepares the ability for casts
    public virtual void Cast()
    {
        if (!IsReady || IsCasting)
            return;
        
        IsCasting = true;
        Prepare();
        _cast = null;
        _cast = StartCoroutine(PreCast());
        CastCallBack(this);
    }

    public delegate void CastEvent(Ability ability);

    //Interupts the ability if it is casting
    public virtual bool Cancel()
    {
        if (!IsCasting || !CanInterrupt)
            return false;
        if (_cast != null)
            StopCoroutine(_cast);
        InterruptCast();
        return true;
    }

    //Private methods
    //The actuall ability cast
    private IEnumerator PreCast()
    {
        yield return new WaitForSeconds(PreCastTime);
        Execute();
        _cast = null;
        _cast = StartCoroutine(PostCast());

    }

    //Waits for the postcasttime to run out so any ability animations can properly finish
    private IEnumerator PostCast()
    {
        yield return new WaitForSeconds(PostCastTime);
        _cast = null;
        IsCasting = false;
        Timestamp = Time.time + Cooldown;
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
        Finish();
    }

    private void LateUpdate()
    {
        ExtendedLateUpdate();
        Release = false;
    }

    
    protected void GenericHit(int viewId, float damage, Vector3 force, float duration, bool knock = true)
    {
        if (_player.IsOwner)
        {
            photonView.RPC("RpcHit", PhotonTargets.All, viewId, damage, force, duration, knock);
        }
    }

    [PunRPC]
    protected void RpcHit(int viewId, float damage, Vector3 force, float duration, bool knock)
    {
        var result = PhotonView.Find(viewId).gameObject;
        if (!result)
            return;
        var comp = result.GetComponent<Target>();
        comp.Hit(damage);
        if (knock)
            comp.KnockDown(force, transform, duration, false, true);
    }

    //Virtual and Abstract methods
    /// <summary>
    /// Start Animation
    /// </summary>
    protected virtual void Prepare() { }
    /// <summary>
    /// Execute ability
    /// </summary>
    protected virtual void Execute() { }
    /// <summary>
    /// End animation and cleanup
    /// </summary>
    protected virtual void Finish() { }
    /// <summary>
    /// End animation and cleanup
    /// </summary>
    protected virtual void InterruptCast() { }
    protected virtual void ExtendedLateUpdate() { }
        
    
}