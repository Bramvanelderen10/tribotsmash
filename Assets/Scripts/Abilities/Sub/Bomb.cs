using System.Collections;
using System.Collections.Generic;
using Tribot;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [System.Serializable]
    public class Implosion
    {
        public float Duration = .8f;
        public float PrepareDuration = .2f;

        public ParticleSystem Glow;
        public ParticleSystem ImplodingParticles;
        public CustomClip ImplosionClip;

        private bool _enabled = false;
        private float _timestamp = 0f;
        private AudioSource _audio;

        public float TotalDuration
        {
            get { return Duration + PrepareDuration; }
        }

        public void Burst(AudioSource audio)
        {
            _audio = audio;
            var emission = ImplodingParticles.emission;
            emission.enabled = true;
            _enabled = true;
            _timestamp = Time.time + PrepareDuration;
        }

        public void Update()
        {
            if (_enabled)
            {
                if (Time.time > _timestamp)
                    SecondWave(_audio);
            }
        }

        void SecondWave(AudioSource audio)
        {
            ImplosionClip.Play(audio);
            Glow.Emit(1);
            var emission = ImplodingParticles.emission;
            emission.enabled = false;
            _enabled = false;
        }
    }

    [System.Serializable]
    public class Explosion
    {
        public ParticleSystem Glow;
        public ParticleSystem Smoke;
        public ParticleSystem ExplodingParticles;
        public ParticleSystem Ring;
        public CustomClip ExplosionClip;

        public int ExplodeParticleCount = 1000;
        public int SmokeCount = 5;

        public void Burst(AudioSource audio)
        {
            Glow.Emit(1);
            Ring.Emit(1);
            Smoke.Emit(SmokeCount);
            ExplodingParticles.Emit(ExplodeParticleCount);
            ExplosionClip.Play(audio);
        }
    }

    public Implosion ImplosionFx;
    public Explosion ExplosionFx;

    public float Radius = 8f;
    public float Damage = 3f;
    public float Force = 10f;

    public bool TEST = false;

    private GameObject _origin = null;
    private bool _imploding = false;
    private bool _exploding = false;
    private AudioSource _audio;

    // Use this for initialization
    void Start ()
    {
        _audio = gameObject.AddComponent<AudioSource>();
    }



	// Update is called once per frame
	void Update ()
	{
	    if (TEST)
	    {
	        TEST = false;
	        TriggerExplosion();
	    }
	    ImplosionFx.Update();
	}

    public void TriggerExplosion(GameObject origin = null)
    {
        _origin = origin;
        ImplosionFx.Burst(_audio);
        StartCoroutine(Explode());
        Destroy(gameObject, 4f);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(ImplosionFx.TotalDuration);
        ExplosionFx.Burst(_audio);

        foreach (var item in Physics.OverlapSphere(transform.position, Radius))
        {
            if (_origin && item.gameObject == _origin)
                continue;

            var obj = item.gameObject;
            var target = obj.GetComponent<Target>();
            if (target)
            {

                obj.transform.LookAt(new Vector3(transform.position.x, obj.transform.position.y, transform.position.z));
                target.KnockDown();
                target.Hit(Damage);
            }

            var rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                var dir = (item.transform.position - transform.position).normalized;
                rb.velocity += dir*Force;
            }

        }

    }
}
