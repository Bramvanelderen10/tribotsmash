using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    public bool OnAwake = true;

    private CustomParticleBursts _particles;

	// Use this for initialization
	void Start ()
	{
        _particles = GetComponent<CustomParticleBursts>();
	    if (OnAwake)
	    {
	        Execute();
	    }
    }

    void Update()
    {
        
    }

    public void Execute(bool destroy = true)
    {
        _particles.Execute();

        if (destroy)
            Destroy(gameObject, 1.5f);
    }
}
