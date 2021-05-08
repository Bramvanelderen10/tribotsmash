using Tribot;
using UnityEngine;

public abstract class Target : TribotBehaviour
{
    [SerializeField]
    private float MaxDistanceCorrection = 1f;
    private OnHit _del;

    public delegate void OnHit();

    public Hitpoints HP { private get; set; }

    public void AddEventListener(OnHit del)
    {
        _del = del;
    }

    public void ClearEventListener()
    {
        _del = null;
    }

    void SendOnHitEvent()
    {
        if (_del != null)
            _del();
    }

    public virtual void Hit(float damage)
    {
        SendOnHitEvent();

        if (HP != null)
            HP.AddDamage(damage);
    }

    public virtual void KnockDown(Vector3 force, Transform lookAt)
    {
        SendOnHitEvent();
    }

    public virtual void KnockDown()
    {
        SendOnHitEvent();
    }

    public virtual void KnockDown(Vector3 force, Transform lookAt, float delay, bool velocity = false, bool forceKnock = false)
    {
        SendOnHitEvent();
    }

    public virtual void KnockDown(Vector3 force, float delay, bool velocity = false)
    {
        SendOnHitEvent();
    }

    public virtual void Slow(float duration, float amount)
    {
        SendOnHitEvent();
    }

    public virtual bool Stun(float duration, bool force = false)
    {
        SendOnHitEvent();
        return true;
    }

    public virtual bool CanStun()
    {
        return true;
    }

    public virtual void Kill()
    {
        SendOnHitEvent();
    }
}

