using UnityEngine;
using System.Collections;
using System;

public class ShieldHitpoints : MonoBehaviour, Hitpoints
{
    public float Hitpoints { get; set; }
    public bool IsAlive { get { return Hitpoints > 0f; } }

    public bool AddDamage(float damage)
    {
        Hitpoints -= damage;
        return IsAlive;
    }

    void Start()
    {
        var comp = GetComponent<Target>();
        if (comp)
            comp.HP = this;
    }
}
