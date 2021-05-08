using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class ParticleOrbit : MonoBehaviour
{

    [SerializeField] private float RotationSpeed = 300f;
    [SerializeField] private float MoveSpeed = 300f;
    [SerializeField] private bool _inwardOrbit = true;

    private ParticleSystem _system;
    private ParticleSystem.Particle[] _particles;
    private Vector3[] _particlePositions;


    public float AdditiveDirectionalSpeed = 0f;


    void Start()
    {
        _system = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_system.maxParticles];
        _particlePositions = new Vector3[_system.maxParticles];
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        int count = _system.GetParticles(_particles);

        for (int i = 0; i < count; i++)
        {
            var pos = Vector3.zero;
            var rot = Quaternion.identity;
            if (_system.main.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                pos = transform.position;
                rot = transform.rotation;
            }
            _particlePositions[i] = _particles[i].position;
            var addedVelocity = (_particles[i].position - pos).normalized*AdditiveDirectionalSpeed;

            if (_inwardOrbit)
            {
                //Bugged but cool looking effect
                var distance = Vector3.Distance(transform.position, _particles[i].position);
                var rot3D = _particles[i].rotation3D;
                rot3D.y += RotationSpeed*Time.deltaTime;
                _particles[i].rotation3D = rot3D;
                _particles[i].velocity = (((Quaternion.Euler(rot3D)*Vector3.forward)*distance) - _particlePositions[i]) + addedVelocity;
            }
            else
            {
                //Intended orbit effect
                var dir = _particles[i].position - pos; // find current direction relative to center
                dir = Quaternion.AngleAxis(1f, rot * Vector3.up) * dir; // rotate the direction
                var worldPos = pos + dir; // define new position
                var velocity = worldPos - _particlePositions[i];
                _particles[i].velocity = (velocity.normalized * MoveSpeed) + addedVelocity;
            }
            

        }
        _system.SetParticles(_particles, count);
    }
}