using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBar : MonoBehaviour, IHealthBar
{
    [SerializeField]
    private Vector3 _position = new Vector3(0f, 0.74f, 0f);
    [SerializeField]
    private Vector3 _scale = new Vector3(4f, 2f, 4f);
    private MeshRenderer _mr;   

    // Use this for initialization
    void Start () {
        _mr = GetComponent<MeshRenderer>();
        _mr.material.SetFloat("_Health", 1f);
        transform.localScale = _scale;
        transform.localPosition = _position;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    transform.rotation = Quaternion.identity;
	}

    public void SetHp(float value)
    {
        _mr.material.SetFloat("_Health", value);
    }
}
