using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleBursts : MonoBehaviour {

    [System.Serializable]
    public struct CustomParticleSystem
    {
        public ParticleSystem Ps;
        public List<Burst> Bursts;
    }
    
    [System.Serializable]
    public struct Burst
    {
        public float Time;
        public int Count;
    }

    public List<CustomParticleSystem> ParticleSystems;
    public bool TestBurst = false;

	// Use this for initialization
	void Start () {
        foreach (var system in ParticleSystems)
        {
            var emission = system.Ps.emission;
            emission.rateOverTime = 0;
            emission.enabled = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
	    if (TestBurst)
	    {
	        Execute();
	        TestBurst = false;
	    }
	}

    public void Execute()
    {
        foreach (var system in ParticleSystems)
        {
            var burstArray = new ParticleSystem.Burst[system.Bursts.Count];
            float maxTime = 1f;
            for (var i = 0; i < system.Bursts.Count; i++)
            {
                if (system.Bursts[i].Time > maxTime)
                {
                    maxTime = system.Bursts[i].Time;
                }
                burstArray[i] = new ParticleSystem.Burst();
                burstArray[i].minCount = (short)system.Bursts[i].Count;
                burstArray[i].maxCount = (short)system.Bursts[i].Count;
                burstArray[i].time = system.Bursts[i].Time;
            }
            StartCoroutine(StartEmission(system, maxTime));
        }
    }

    IEnumerator StartEmission(CustomParticleSystem system, float time)
    {
        foreach (var burst in system.Bursts)
        {
            yield return new WaitForSeconds(burst.Time);
            system.Ps.Emit(burst.Count);
        }

        
    }
}
