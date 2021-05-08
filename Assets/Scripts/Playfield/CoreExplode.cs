using UnityEngine;
using Tribot;
using System.Collections.Generic;

/// <summary>
/// Lets the core do the explosion animation and logic
/// Multiplayer synced
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class CoreExplode : Target
{
    enum Type
    {
        OnHit,
        Interval
    }

    [Header("General Explode Settings")]
    [SerializeField] private AnimationCurve _explodeIntensityCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _maxRadius = 5.5f;
    [SerializeField] private float _waveWidth = 1f;
    [SerializeField] private float _expandSpeed = 6f;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private bool _knockback = false;
    [SerializeField] private float _knockbackSpeed = 3f;
    [SerializeField] private float _knockbackDuration = .15f;
    [SerializeField, MinMaxRange(0, 60)] private Range _explodeInterval = new Range() {Min = 10, Max = 30};
    [SerializeField] private Type _type = Type.Interval;

    [Header("Orbit Particles Settings")]
    [SerializeField] private ParticleOrbit _orbit;
    [SerializeField] private float _outOfOrbitSpeed = 5f;

    [Header("Core Lighting Settings")]
    [SerializeField] private CoreController _core;

    [SerializeField] private float _colorIntensity = 2f;

    [Header("Shockwave")]
    [SerializeField] private ParticleSystem _shockwave;
    [SerializeField] private int _emitCount = 2;

    private float _lastExplode = 0f;
    private float _nextTick;
    private bool _explode = false;
    private List<Target> _hitTargets = new List<Target>();

    private void Start()
    {
        _nextTick = _explodeInterval.Random;
        _lastExplode = 0;
        AddEventListener(ExplodeEvent);
    }

    private void Update()
    {
        if ((_type == Type.Interval) ? Time.time > _nextTick : _explode && IsMaster)
        {
            photonView.RPC("Explode", PhotonTargets.All, _explodeInterval.Random);
        }

        if (Time.time - _lastExplode < _duration && Time.time > 1f)
        {
            var evaluationTime = (Time.time - _lastExplode)/_duration;
            var explodeIntensity = _explodeIntensityCurve.Evaluate(evaluationTime);

            if (_core)
                _core.ColorIntensity = (explodeIntensity *_colorIntensity)+ 1f;

            if (_orbit)
                _orbit.AdditiveDirectionalSpeed = explodeIntensity*_outOfOrbitSpeed;

            var inverse = 1f - explodeIntensity;
            var outerResults = Physics.OverlapSphere(transform.position, _maxRadius*inverse);
            foreach (var result in outerResults)
            {
                if (Vector3.Distance(result.transform.position, transform.position) < (_maxRadius * inverse) - _waveWidth)
                    continue;

                var target = result.GetComponent<Target>();
                
                if (target  && target != this && !_hitTargets.Contains(target))
                {
                    _hitTargets.Add(target);
                    if (target.GetType() == typeof (Player))
                    {
                        var player = (Player) target;
                        if (player.IsOwner)
                        {
                            photonView.RPC("Hit", PhotonTargets.All, player.Index);
                        }
                    }
                    else
                    {
                        Hit(target);
                    }
                }
            }
        }
        else
        {
            if (_core)
                _core.ColorIntensity = 1f;

            if (_orbit)
                _orbit.AdditiveDirectionalSpeed = 0f;
        }

    }

    public void ExplodeEvent()
    {
        _explode = true;
    }

    /// <summary>
    /// Executes explosion animation on all clients
    /// </summary>
    /// <param name="interval"></param>
    [PunRPC]
    public void Explode(float interval)
    {
        _hitTargets.Clear();
        _explode = false;
        _nextTick = interval + Time.time;
        _lastExplode = Time.time;
        _shockwave.startLifetime = _duration;
        _shockwave.Emit(_emitCount);
    }

    void Hit(Target target)
    {
        var dir = transform.Direction(target.transform, true);
        dir.y = 1f;
        target.Hit(_damage);
        if (_knockback)
            target.KnockDown(dir * _knockbackSpeed, transform, .3f, true);
    }

    [PunRPC]
    void Hit(int index)
    {
        var player = Container<IPlayerManager>.Instance.Items.Find(x => x.Index == index);
        if (player == null)
            return;

        var target = player.PlayerObject.GetComponent<Target>();
        Hit(target);
    }
}