using UnityEngine;
using System.Collections;

public class ReturningProjectile : MonoBehaviour
{
    public bool IsReleased = false;
    public Transform Origin { set; private get; }
    public float MaxDistance = 10f;
    public float BaseSpeed = 50f;
    public float MinimumSpeed = 10f;
    public float Damage = 5f;
    public float KnockbackForce = 200f;
    public float ProjectileRadius = 0.5f;

    private bool _returning = false;
    private Vector3 _startPos;

    public float FalloffDistance = 0.5f;

    private float Multiplier
    {
        get { return ((MaxDistance - Distance) / FalloffDistance >= 1) ? 1 : (MaxDistance - Distance) / FalloffDistance; }
    }

    private float AdjustedSpeed
    {
        get { return (BaseSpeed * Multiplier >= MinimumSpeed)? BaseSpeed * Multiplier: MinimumSpeed; }
    }

    private float Distance { get { return Vector3.Distance(_startPos, transform.position); } }
    private float DistanceOrigin { get { return Vector3.Distance(Origin.position, transform.position); } }

	// Use this for initialization
	void Start ()
	{
	    _startPos = Origin.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!IsReleased)
	        return;

	    if (!_returning)
	    {
	        RaycastHit hit;
	        if (Physics.SphereCast(transform.position, ProjectileRadius, transform.forward, out hit, AdjustedSpeed * Time.deltaTime + 0.1f))
	        {
	            if (hit.transform != Origin)
	            {
	                var hitPoints = hit.transform.GetComponent<Hitpoints>();
	                if (hitPoints != null)
	                {
	                    hitPoints.AddDamage(Damage);
	                }

	                var rb = hit.transform.GetComponent<Rigidbody>();
	                if (rb != null)
	                {
                        rb.AddForce(transform.forward * KnockbackForce);
                    }

                    _returning = true;
                    return;
                }
	            
	        }

	        transform.position += transform.forward*Time.deltaTime* AdjustedSpeed;
	        if (Distance >= MaxDistance)
	        {
	            _returning = true;
	        }
	    }
	    else
	    {
	        transform.position = Vector3.MoveTowards(transform.position, Origin.position, AdjustedSpeed * Time.deltaTime);
	        transform.LookAt(Origin);

	        if (DistanceOrigin <= 1f)
	        {
	            Destroy(gameObject);
	        }
	    }
	}
}
