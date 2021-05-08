using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages rotation and color of the core
/// </summary>
public class CoreController : MonoBehaviour {

    [SerializeField] private float _speed = 60f;
    [SerializeField] private float _baseIntensity;

    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private string _colorIntensityField = "_ColorIntensity";

    [HideInInspector]public float ColorIntensity = 1f;

    private Material _material;

    void Start()
    {
        _mesh.material = new Material(_mesh.material);
        _baseIntensity = _mesh.material.GetFloat(_colorIntensityField);
        _material = _mesh.material;

    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, _speed * Time.deltaTime, 0f));
        _material.SetFloat(_colorIntensityField, _baseIntensity*ColorIntensity);
    }
}