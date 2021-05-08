using UnityEngine;
using System.Collections;

public class Shield : Target
{

    public float BaseScale = 6f;
    public float ScaleMultiplier = 1f;
    public float LerpRateShield = 1.5f;

    private Hitpoints _hp;

    void Start()
    {
        _hp = GetComponent<Hitpoints>();
    }

	// Update is called once per frame
	void Update ()
	{
        
	    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * (BaseScale * ScaleMultiplier), LerpRateShield);

    }

    public override void Hit(float damage)
    {
        base.Hit(damage);

    }
}
