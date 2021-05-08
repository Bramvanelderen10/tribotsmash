using System.Collections;
using UnityEngine;
using Tribot;

public class ObjectiveItem : TribotBehaviour
{
    [SerializeField] private Vector3 _posOnHold = new Vector3(0f, 3f, 0f);
    [SerializeField] private Vector3 _posOnDrop = new Vector3(0f, .5f, 0f);
    [SerializeField] private GameObject _model;

    private float _scoreInterval = .5f;
    private float _cooldown = .5f;
    private float _scoreRate = 2f;
    private SendScore _del;

    private float _timestamp = 0f;
    private PlayerInfo _target;
    private Coroutine _cr;

    public delegate void SendScore(int index, float score);

    // Use this for initialization
    public void Initialise(float scoreRate, float scoreInterval, float cooldown, SendScore del)
    {
        _del = del;
        _scoreRate = scoreRate;
        _scoreInterval = scoreInterval;
        _cooldown = cooldown;
	    _cr = StartCoroutine(AddScore());
	}

    void Update()
    {
        if (_target != null)
            _model.transform.localPosition = Vector3.MoveTowards(_model.transform.localPosition, _posOnHold, .5f);
        else
            _model.transform.localPosition = Vector3.MoveTowards(_model.transform.localPosition, _posOnDrop, .5f);
    }

    void OnTriggerEnter(Collider other)
    {
        Attach(other);
    }

    void OnTriggerStay(Collider other)
    {
        Attach(other);
    }

    void Attach(Collider other)
    {
        if (!IsMaster)
            return;

        if (Time.time <= _timestamp)
            return;
        
        if (_target != null)
            return;

        if (!other.CompareTag("Player"))
            return;

        var target = other;
        var player = target.GetComponent<PlayerInfo>();
        photonView.RPC("AttachNetworked", PhotonTargets.All, player.Index);
    }

    [PunRPC]
    void AttachNetworked(int index)
    {
        foreach (var item in Container<IPlayerManager>.Instance.Items)
        {
            if (item.Index == index)
            {
                var obj = item.PlayerObject;
                var player = obj.GetComponent<PlayerInfo>();
                _target = player;
                obj.GetComponent<Target>().AddEventListener(EventReciever);

                transform.parent = _target.transform;
                transform.localPosition = Vector3.zero;
            }
        }

    }

    void Detach()
    {
        if (!IsMaster)
            return;

        photonView.RPC("DetachNetworked", PhotonTargets.All);
    }

    [PunRPC]
    void DetachNetworked()
    {
        _timestamp = Time.time + _cooldown;

        if (_target == null)
            return;

        _target.gameObject.GetComponent<Target>().ClearEventListener();
        transform.parent = null;
        _target = null;
    }


    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(_scoreInterval);

        if (_target && _del != null)
        {
            var index = _target.Index;
            TriLog.Log("Player", index, "Add score" + (_scoreRate));
            _del(index, _scoreRate);
        }

        _cr = StartCoroutine(AddScore());
    }

    public void EventReciever()
    {
        Detach();
    }
}
