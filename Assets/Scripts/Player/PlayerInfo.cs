using UnityEngine;
using Tribot;

/// <summary>
/// Syncs the player and relevant info across the network and gets used to send data around to other player scripts
/// </summary>
public class PlayerInfo : TribotBehaviour
{
    public int Index = 0;
    public bool Ai = false;

    public bool IsOwner
    {
        get { return (PhotonNetwork.offlineMode || ComparePlayerIndex(Index) || (Ai && IsMaster)); }
    }

    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _rotationSpeed = 12f;
    [SerializeField] private float _maxInterpolateDistance = 3f;

    private Player _player;
    private Rigidbody _rb;

    /// <summary>
    /// Network Variables
    /// </summary>
    // cached values for correct position/rotation (which are then interpolated)
    private Quaternion correctPlayerRot;
    private Vector3 currentVelocity;
    private float updateTime = 0;

    private float _lastSyncTime = 0f;
    private float _syncDelay = 0f;
    private float _syncTime = 0f;
    private Vector3 _syncStartPos = Vector3.zero;
    private Vector3 _syncEndPos = Vector3.zero;
    private Quaternion _syncStartRot = Quaternion.identity;
    private Quaternion _syncEndRot = Quaternion.identity;

    void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PhotonNetwork.offlineMode)
            return;

        if ((int) PhotonNetwork.player.CustomProperties["Index"] != Index && !(PhotonNetwork.isMasterClient && Ai))
        {
            //Sync rotation and movement
            _syncTime += Time.deltaTime;
            
            //Prevent floating if a teleport gets synced incorrectly or late due to ping
            if (Vector3.Distance(_syncStartPos, _syncEndPos) > _maxInterpolateDistance)
            {
                transform.position = _syncEndPos;
            }
            else
            {
                transform.position = Vector3.Lerp(_syncStartPos, _syncEndPos, _syncTime/_syncDelay);
            }
            transform.rotation = Quaternion.Lerp(_syncStartRot, _syncEndRot, _syncTime/_syncDelay);
        }
            
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(_rb.velocity);
            stream.SendNext(Index);
        }
        else {
            
            var syncPos = (Vector3)stream.ReceiveNext();
            var syncRot = (Quaternion)stream.ReceiveNext();
            var syncVel = (Vector3)stream.ReceiveNext();
            Index = (int) stream.ReceiveNext();

            _syncTime = 0f;
            _syncDelay = Time.time - _lastSyncTime;
            _lastSyncTime = Time.time;

            _syncEndPos = syncPos + syncVel*_syncDelay;
            _syncStartPos = transform.position;

            _syncEndRot = syncRot;
            _syncStartRot = transform.rotation;

            updateTime = Time.time;
        }
    }
}
