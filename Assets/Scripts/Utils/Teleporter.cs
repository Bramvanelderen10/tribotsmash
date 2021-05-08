using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tribot;

[RequireComponent(typeof(CustomParticleBursts))]
public class Teleporter : MonoBehaviour
{
    public GameObject Destination;
    public float Delay = 0.5f;
    public float Cooldown = 0.5f;
    public List<string> Tags = new List<string> {"Player"};
    public CustomClip ClipEnter;
    public CustomClip ClipExit;

    private AudioSource _audio;
    private CustomParticleBursts _particles;
    private List<GameObject> _TeleporterObjects = new List<GameObject>();
    private bool _activated = false;
    private float _timestamp = 0f;

	// Use this for initialization
	void Start ()
	{
	    _audio = gameObject.AddComponent<AudioSource>();
        _particles = GetComponentInChildren<CustomParticleBursts>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (!Destination)
            return;

        var obj = other.gameObject;

        if (!Tags.Contains(obj.tag))
            return;

        _TeleporterObjects.Add(obj);

        if (_activated || _timestamp > Time.time)
            return;

        _activated = true;
        TriggerEffects();
        StartCoroutine(StartTeleporting());
    }

    void OnTriggerExit(Collider other)
    {
        if (_TeleporterObjects.Contains(other.gameObject))
            _TeleporterObjects.Remove(other.gameObject);
    }

    public void TriggerEffects(bool exit = false)
    {
        if (!_audio.isPlaying)
        {
            if (exit)
            {
                ClipExit.Play(_audio);
            }
            else
            {
                ClipEnter.Play(_audio);
            }
        }

        _timestamp = Time.time + Cooldown;
        _particles.Execute();
    }

    IEnumerator StartTeleporting()
    {
        yield return new WaitForSeconds(Delay);
        var tComp = Destination.GetComponentInChildren<Teleporter>();

        foreach (var obj in _TeleporterObjects)
        {
            var position = Vector3.zero;
            position.x = (obj.transform.position.x - transform.position.x) + Destination.transform.position.x;
            position.y = (obj.transform.position.y - transform.position.y) + Destination.transform.position.y;
            position.z = (obj.transform.position.z - transform.position.z) + Destination.transform.position.z;
            obj.transform.position = position;

        }
        _TeleporterObjects.Clear();

        if (tComp)
        {
            tComp.TriggerEffects(true);
        }

        _activated = false;
    }
}
