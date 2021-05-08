using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using Tribot;

/// <summary>
/// 
/// </summary>
public class Level1Showcase : MonoBehaviour
{
    [Serializable]
    class Path
    {
        public Transform[] Nodes;
        public float Duration;
        
    }


    [SerializeField] private Path _path1;

    [SerializeField] private Transform _lookat;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotSpeed = 100f;

    private Sequence _seq;

    void Start()
    {
        StartCoroutine(CreateSequence());
    }

    void LateUpdate()
    {
        var rot = transform.LookRotation(_lookat.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, _rotSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, _target.position, _speed*Time.deltaTime);
    }

    IEnumerator CreateSequence()
    {
        _seq = DOTween.Sequence();


        Vector3[] nodes = new Vector3[_path1.Nodes.Length];
        for (int i = 0; i < _path1.Nodes.Length; i++)
        {
            nodes[i] = _path1.Nodes[i].position;
        }
        _seq.Append(_target.DOPath(nodes, _path1.Duration, PathType.CatmullRom));

        _seq.Join(_lookat.DOMove(nodes[2], 0f));

        yield return new WaitForSeconds(4);
        _seq.Join(_lookat.DOMove(new Vector3(0, 3, 0), 0f));

        yield return new WaitForSeconds(_path1.Duration - 4);

        StartCoroutine(CreateSequence());
    }
}