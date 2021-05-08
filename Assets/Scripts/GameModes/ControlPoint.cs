using System.Collections.Generic;
using UnityEngine;
using Tribot;

public class ControlPoint : MonoBehaviour
{
    private SendScore _del;
    private float _scoreRate = 1f;
    private float _lifetime = 0f;

    private List<Defender> _defenders = new List<Defender>();
    private MeshRenderer _mr;
    private float _timestamp = 0f;
    private bool _init = false;

    public GameObject Obj
    {
        get { return gameObject; }
    }

    public delegate void SendScore(int index, float score);

    public void Initialise(float lifetime, float scoreRate, SendScore del)
    {
        TargetContainer.Instance.ControlPoints.Add(gameObject);

        _scoreRate = scoreRate;
        _lifetime = lifetime;
        _del = del;
        _mr = GetComponent<MeshRenderer>();
        _timestamp = Time.time + _lifetime;
        _init = true;
    }

    void Update()
    {
        if (_init)
            _mr.material.SetFloat("_Health", (_timestamp - Time.time)/_lifetime);
    }

    void FixedUpdate()
    {
        foreach(var defender in _defenders)
        {
            defender.Score += _scoreRate * Time.deltaTime;
            if (defender.Score >= _scoreRate)
            {
                _del(defender.Index, defender.Score);
                defender.Score = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_init)
            return;

        var obj = other.gameObject;
        var player = obj.GetComponent<PlayerInfo>();
        if (player)
        {
            _defenders.Add(new Defender(player.Index, 0f));
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!_init)
            return;

        var obj = other.gameObject;
        var player = obj.GetComponent<PlayerInfo>();
        if (player)
            _defenders.RemoveAll(d => d.Index == player.Index);        
    }

    void OnDestroy()
    {
        TargetContainer.Instance.ControlPoints.Remove(gameObject);
    }

    private class Defender
    {
        public Defender(int index, float score)
        {
            Index = index;
            Score = score;
        }

        public int Index;
        public float Score;
    }
}
