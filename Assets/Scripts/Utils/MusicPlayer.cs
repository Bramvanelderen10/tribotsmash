using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tribot;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private CustomClip _music;
    private AudioSource _audio;
    private bool _play = true;

    public bool Play
    {
        get { return _play; }
        set
        {
            if (!value)
            {
                _audio.Stop();
            }

            _play = value;
        }
    }

	// Use this for initialization
	void Start ()
	{
	    _audio = gameObject.AddAudioSource();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!_audio.isPlaying && Play)
	        _music.Play(_audio);
	}
}
