using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFx : MonoBehaviour
{
    public bool EmittersEnabled
    {
        get { return _emittersEnabled; }
        set
        {
            _emittersEnabled = value;
            foreach (var item in _particles)
            {
                var emission = item.emission;
                emission.enabled = _emittersEnabled;
            }
        }
    }

    private bool _emittersEnabled = false;
    public List<ParticleSystem> _particles = new List<ParticleSystem>();
}
