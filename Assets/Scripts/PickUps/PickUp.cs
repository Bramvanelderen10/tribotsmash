using UnityEngine;
using Tribot;

/// <summary>
/// The base pickup class responsible for the default pickup logic
/// Derive from this class to create any pickup
/// </summary>
public abstract class PickUp : TribotBehaviour
{
    [SerializeField] private PickUpData _data;
    [SerializeField] private GameObject _pickupFx;
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private bool _startCounting = false;
    private float _deathTimer = 0f;

    protected SpriteRenderer Sprite;

    protected virtual void Start()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialise(Vector3 position)
    {
        TargetContainer.Instance.Pickups.Add(gameObject);
        Vector3 startPos = new Vector3(0f, 0f, 0f);
        startPos.x = position.x;
        startPos.y = position.y + _data.StartHeightOffset;
        startPos.z = position.z;

        _targetPosition = position;
        transform.position = startPos;
        _isMoving = true;
    }

	void Update ()
    {
	    if (_isMoving)
	    {
	        transform.position -= transform.up*Time.deltaTime* _data.DropSpeed;

	        if (Vector3.Distance(_targetPosition, transform.position) < 0.2f)
	        {
	            _isMoving = false;
	            _startCounting = true;
	            _deathTimer = Time.time + _data.LifeTime;
	        }
	    }
	    else
	    {
	        var pos = transform.position;
	        pos.y = _targetPosition.y + (Mathf.Sin(Time.time * _data.IdleSpeed) * _data.CurveHeight);
	        transform.position = pos;
	    }

	    if (_startCounting)
	    {
	        if (Time.time >= _deathTimer)
	            Destroy(this.gameObject);
	    }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            photonView.RPC("Execute", PhotonTargets.All, other.gameObject.GetComponent<PlayerInfo>().Index);
        }
    }

    void OnDestroy()
    {
        TargetContainer.Instance.Pickups.Remove(gameObject);
    }

    [PunRPC]
    protected void Execute(int index)
    {
        foreach (var item in Container<IPlayerManager>.Instance.Items)
        {
            if (item.Index == index)
            {
                var obj = Instantiate(_pickupFx);
                obj.transform.parent = item.PlayerObject.transform;
                obj.transform.localPosition = new Vector3(0, 1, 0);
                ExtendedExecute(item);
                Destroy(gameObject);
            }
        }


    }

    protected abstract void ExtendedExecute(IPlayerManager player);
}
