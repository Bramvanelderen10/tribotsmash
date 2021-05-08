using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tribot;

public class PickupFx : MonoBehaviour
{
    [SerializeField] private CustomClip _clip;
    private List<CustomParticleBursts> _ps = new List<CustomParticleBursts>();

	// Use this for initialization
	void Start ()
	{
        Destroy(gameObject, 2f);
        if (_clip != null)
	        _clip.Play(gameObject.AddAudioSource());

	    foreach (var item in GetComponentsInChildren<CustomParticleBursts>())
	    {
	        _ps.Add(item);
	        item.Execute();
	        
	    }
	}
}
