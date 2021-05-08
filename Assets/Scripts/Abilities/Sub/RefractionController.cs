using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionController : MonoBehaviour
{

    private ParticleSystem _ps;
    private Renderer _psr;

	// Use this for initialization
	void Start ()
	{
	    _ps = GetComponent<ParticleSystem>();
        _psr = _ps.GetComponent<Renderer>();
        _psr.material = new Material(_psr.material);
        _psr.material.SetFloat("_Opacity", 1);

    }
	
	// Update is called once per frame
	void Update () {
        List<Vector4> list = new List<Vector4>();
        _ps.GetCustomParticleData(list, ParticleSystemCustomData.Custom1);
	    if (list.Count == 0)
	        return;

	    var value = list[0].x;
	    if (value > 1)
	    {
	        value = 1f;
	    } else if (value < 0)
	    {
	        value = 0f;
	    }

	    _psr.material.SetFloat("_Opacity", value);
	}
}
