using UnityEngine;
using System.Collections;
using Tribot;

public class ShockBlast : TribotBehaviour
{
    [HideInInspector]
    public bool IsReleased = false;

    [Header("Settings")]
    public float Speed = 30f;
    public float Radius = 0.5f;
    public float Damage = 0.2f;
    public float StunDuration = 0.01f;
    public float KnockbackForce = 300f;

    [Header("Others")]
    public Transform Origin;
    public GameObject ShockBlastExplosionPrefab;
    public CustomClip ClipCharge;
    public CustomClip ClipHit;

    [SerializeField] private ParticleSystem _system;
    private ParticleSystem.ShapeModule _shape;

    [HideInInspector]
    public bool Original = false;

    private AudioSource _audio;


    void Start()
    {
        TargetContainer.Instance.Projectiles.Add(gameObject);
        _audio = gameObject.AddAudioSource();
        ClipCharge.Play(_audio);

        _shape = _system.shape;
        StartCoroutine(ScaleParticleSystem());
    }

	// Update is called once per frame
	void Update ()
	{
        if (!IsReleased)
            return;

	    RaycastHit hit;
	    if (Physics.SphereCast(transform.position, Radius, transform.forward, out hit,
	        Speed * Time.deltaTime + 0.1f))
	    {
	        if (hit.transform != Origin)
	        {

	            var target = hit.transform.GetComponent<Target>();
	            if (target != null && Original)
	            {
	                photonView.RPC("Hit", PhotonTargets.All, target.photonView.viewID);
	            }

                //var results = Physics.OverlapSphere(hit.point, Radius * 2);
                //foreach (var result in results)
                //{
                //       var rb = result.transform.GetComponent<Rigidbody>();
                //       if (rb != null)
                //       {
                //           var rotation = Quaternion.LookRotation((result.transform.position - hit.point).normalized);
                //           rb.AddForce(rotation * Vector3.forward * KnockbackForce);
                //       }
                //   }

                var obj = Instantiate(ShockBlastExplosionPrefab);
                //var comp = obj.GetComponent<HitEffects>();
                obj.transform.position = hit.point;
                ClipHit.Play(obj.AddComponent<AudioSource>());

                Destroy(gameObject);
            }
	    }

	    transform.position += transform.forward*Speed*Time.deltaTime;
	}

    IEnumerator ScaleParticleSystem()
    {
        _shape.radius = .01f;
        while (_shape.radius < .5f)
        {
            _shape.radius += .25f * Time.deltaTime;
            yield return null;
        }
    }

    [PunRPC]
    void Hit(int viewId)
    {
        var result = PhotonView.Find(viewId).gameObject;
        var target = result.GetComponent<Target>();
        if (target)
        {
            target.Hit(Damage);
            target.Stun(StunDuration);
        }
        
    }

    void OnDestroy()
    {
        TargetContainer.Instance.Projectiles.Remove(gameObject);
    }
}
