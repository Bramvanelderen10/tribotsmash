using System.Collections;
using System.Collections.Generic;
using Tribot;
using UnityEngine;

public class WallPiece : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 Extends = new Vector3(2f, 0, 2f);
    [MinMaxRange(0f, 20f)]
    public Range TargetHeight;
    public float DeActivationHeight = -10f;
    public float SwitchTime = 0.5f;
    public float UpwardSpeed = 3f;
    public float DownwardSpeed = 1f;
    public float PushVelocity = 10f;

    [Header("Audio")]
    public Range PitchRange;
    public CustomClip ClipSpawn;
    public CustomClip ClipRetract;


    [Header("Prefab")]
    public GameObject Fx;

    private bool _active = false;
    private bool _goingUp = true;
    private float _targetHeight = 0f;

	
	// Update is called once per frame
	void Update () {
	    if (_active)
	    {
	        if (_goingUp)
	        {

	            var pos = transform.position;
	            pos.y = Mathf.MoveTowards(pos.y, _targetHeight, UpwardSpeed * Time.deltaTime);
	            transform.position = pos;

	            if (transform.position.y >= _targetHeight)
	            {
	                _active = false;
	                _goingUp = false;
	                StartCoroutine(Activate(SwitchTime));
	            }
	        }
	        else
	        {
	            var pos = transform.position;
	            pos.y = Mathf.MoveTowards(pos.y, DeActivationHeight, DownwardSpeed * Time.deltaTime);
	            transform.position = pos;

	            if (transform.position.y <= DeActivationHeight)
	            {
	                Destroy(gameObject);
	            }
	        }
	    }
	}

    public void Activate()
    {
        _active = true;
        var obj = Instantiate(Fx);
        obj.transform.position = transform.position;
        _goingUp = true;

        float additive = Random.Range(TargetHeight.Min, TargetHeight.Max);
        _targetHeight = transform.position.y + additive;
        PlayAudio(additive);
    }

    private void PlayAudio(float additive)
    {
        var source = gameObject.AddComponent<AudioSource>();
        var percentage = (additive - TargetHeight.Min) / (TargetHeight.Max - TargetHeight.Min);
        var offset = (PitchRange.Max - PitchRange.Min) * percentage;
        var pitch = PitchRange.Max - offset;
        ClipSpawn.Pitch.Min = pitch;
        ClipSpawn.Pitch.Max = pitch;
        ClipSpawn.Play(source);
    }

    IEnumerator Activate(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        var source = gameObject.AddComponent<AudioSource>();
        ClipRetract.Pitch = ClipSpawn.Pitch;
        ClipRetract.Play(source);

        _active = true;
    }
}
