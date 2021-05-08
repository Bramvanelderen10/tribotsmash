using UnityEngine;
using System.Collections.Generic;
using Tribot;

/// <summary>
/// 
/// </summary>
public class PortalController : TribotBehaviour
{
    [Header("Settings")] [SerializeField] private List<PortalController> _connectedPortals;
    [SerializeField] private float _cooldown = 2f;

    [Header("Player Enter Settings")]
    [SerializeField] private AnimationCurve _alphaCurve = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField] private float _duration = .5f;
    [SerializeField] private ParticleSystemRenderer _borderParticleRenderer;

    [Header("Player Exit Settings")]
    [SerializeField] private float _launchSpeed = 10f;
    [SerializeField] private float _launchDuration = .3f;

    private CustomParticleBursts _customParticles;
    private float _lastTime = 0f;

    void Start()
    {
        _customParticles = GetComponent<CustomParticleBursts>();
        if (_borderParticleRenderer)
        {
            _borderParticleRenderer.material = new Material(_borderParticleRenderer.material);
        }
    }

    void Update()
    {
        if (Time.time > _lastTime && Time.time - _lastTime < _duration)
        {

            var curveTime = (1f/_duration)*(Time.time - _lastTime);
            var alpha = _alphaCurve.Evaluate(curveTime);
            var color = _borderParticleRenderer.material.GetColor("_TintColor");
            color.a = alpha;
            _borderParticleRenderer.material.SetColor("_TintColor", color);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (Time.time <= _lastTime + _cooldown)
            return;

        if (_connectedPortals.Count == 0)
            return;

        var obj = other.gameObject;
        if (!obj.CompareTag("Player"))
            return;

        var comp = obj.GetComponent<PlayerInfo>();
        if (!comp || !comp.IsOwner)
            return;

        var portalIndex = Random.Range(0, _connectedPortals.Count);
        var index = comp.Index;
        photonView.RPC("Enter", PhotonTargets.All, index, portalIndex);
    }

    [PunRPC]
    void Enter(int indexPlayer, int indexPortal)
    {
        var targetPortal = _connectedPortals[indexPortal];
        var targetTransform = targetPortal.transform;

        var something = Container<IPlayerManager>.Instance.Items.Find(x => x.Index == indexPlayer);
        var playerObj = something.PlayerObject;

        var lookDir = Vector3.back;
        playerObj.transform.forward = targetTransform.TransformVector(lookDir);
        playerObj.transform.position = targetPortal.transform.position;

        var player = playerObj.GetComponent<Player>();
        if (player)
            player.KnockDown(targetTransform.TransformVector(Vector3.forward * _launchSpeed), _launchDuration, true);

        EnterFx();
        targetPortal.ExitFx();
    }

    public void AddConnectedPortal(PortalController portal)
    {
        if (_connectedPortals == null)
            _connectedPortals = new List<PortalController>();
        _connectedPortals.RemoveAll(item => item == null);

        _connectedPortals.Add(portal);
    }

    public void EnterFx()
    {
        _lastTime = Time.time;
    }

    public void ExitFx()
    {
        _lastTime = Time.time;
        _customParticles.Execute();
    }
}