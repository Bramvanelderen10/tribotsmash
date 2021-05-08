using UnityEngine;
using Random = UnityEngine.Random;
using Tribot;

/// <summary>
/// The pickupmanager is responsible for spawning random pickups in the playfield
/// It gets created by the gamemode class
/// </summary>
public class PickUpManager : TribotBehaviour
{
    public static PickUpManager Instance;

    public Range PickUpInterval { private get; set; }
    public bool IsEnabled = false;
    public float HorizontalOffset = 3f;
    public float VerticalOffset = 1.5f;
    public LayerMask GroundLayer;
    public LayerMask BorderLayer;

    private float _cooldown = 0;

    public void Enable()
    {
        IsEnabled = true;
        _cooldown = Time.time + PickUpInterval.Random;
    }

    public void Disable()
    {
        IsEnabled = false;
        _cooldown = Time.time + PickUpInterval.Random;
    }

    void Start()
    {
        Instance = this;
    }

	void Update ()
    {
	    if (IsEnabled && Time.time >= _cooldown && IsMaster)
	    {
	        var pickup = PickUpCollection.Instance.GetRandomItemIndex();
            var position = GetPosition();
            position.y += 1f;
            _cooldown = Time.time + PickUpInterval.Random;
	        TriLog.Log(PickUpCollection.Instance.GetItem(pickup).name);
            photonView.RPC("SpawnPickUp", PhotonTargets.All, pickup, position, PhotonNetwork.AllocateViewID());
	    }
	}

    [PunRPC]
    void SpawnPickUp(int pickUp, Vector3 pos, int viewId)
    {
        var obj = Instantiate(PickUpCollection.Instance.GetItem(pickUp));
        var view = obj.AddComponent<PhotonView>();
        view.viewID = viewId;
        var comp = obj.GetComponent<PickUp>();
        if (comp == null)
            return;
        comp.Initialise(pos);
    }

    Info GetExtends()
    {
        var minX = 0f;
        var maxX = 0f;
        var minZ = 0f;
        var maxZ = 0f;

        var i = 0;
        foreach (var player in TargetContainer.Instance.Players)
        {
            var pos = player.transform.position;

            if (i == 0)
            {
                minX = pos.x;
                maxX = pos.x;
                minZ = pos.z;
                maxZ = pos.z;
            }
            else
            {
                if (pos.x > maxX)
                {
                    maxX = pos.x;
                }
                if (pos.x < minX)
                {
                    minX = pos.x;
                }

                if (pos.z > maxZ)
                {
                    maxZ = pos.z;
                }
                if (pos.z < minZ)
                {
                    minZ = pos.z;
                }
            }
            i++;
        }

        var downLeft = new Vector3(minX, 0, minZ);
        var downRight = new Vector3(maxX, 0, minZ);
        var upLeft = new Vector3(minX, 0, maxZ);

        var info = new Info();
        info.Extends = new Vector3(Vector3.Distance(downLeft, downRight) / 2, 0, Vector3.Distance(downLeft, upLeft) / 2);
        info.Center = new Vector3((minX + maxX)/2, 0, (minZ + maxZ)/2);

        return info;
    }

    Vector3 GetPosition()
    {
        var info = GetExtends();
        while (true)
        {
            var x = Random.Range(-(info.Extends.x + HorizontalOffset), (info.Extends.x + HorizontalOffset));
            var z = Random.Range(-(info.Extends.z + VerticalOffset), (info.Extends.z + VerticalOffset));
            var ray = new Ray(new Vector3(x, 30f, z) + info.Center, Vector3.down);

            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 60f, GroundLayer))
            {
                continue;
            }

            var rayBorder = new Ray(hit.point, Vector3.right);
            var hits = Physics.RaycastAll(rayBorder, 100f, BorderLayer);
            if (hits.Length%2 == 0)
            {
                continue;
            }

            return hit.point;
        }
    }

    struct Info
    {
        public Vector3 Extends;
        public Vector3 Center;
    }
}
