using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GrabProjectile : MonoBehaviour
{
    public GameObject Origin;
    public AbilityRangedGrab GrabAbility;

    public float ProjectileSpeed = 20f;
    public float PlayerSpeed = 10f;
    public float Radius = 2f;
    public float Damage = 1f;
    public float MaxRange = 3.5f;
    public float MaxGrabDuration = 2f;

    public AudioClip Launch;
    public AudioClip Hit;

    private bool _isReleased = false;
    private bool _hit = false;
    private GameObject _target;
    private Vector3 _offset;
    private Vector3 _start;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_isReleased)
        {
            if (!_hit)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit,
                    ProjectileSpeed * Time.deltaTime))
                {
                    if (hit.transform != Origin.transform && !hit.collider.isTrigger)
                    {
                        if (hit.transform.GetComponent<Target>() != null)
                        {
                            hit.transform.GetComponent<Target>().Hit(Damage);
                        }
                        _target = hit.collider.gameObject;
                        _offset = hit.point - _target.transform.position;
                        _hit = true;
                        StartCoroutine(FailSafe());
                        GrabAbility.Grappled();
                    }
                }
                else
                {
                    transform.position += transform.forward*Time.deltaTime*ProjectileSpeed;
                    if (Vector3.Distance(transform.position, _start) > MaxRange)
                    {
                        EndProjectile();
                    }
                }
            }
            else
            {
                Origin.transform.position = Vector3.MoveTowards(Origin.transform.position, _target.transform.position + _offset,
                    Time.deltaTime*PlayerSpeed);

                var ray = new Ray();
                ray.origin = _target.transform.position + _offset;
                ray.direction = Origin.transform.position - (_target.transform.position + _offset);
                ray.direction = ray.direction/ray.direction.magnitude;

                foreach (var hit in Physics.RaycastAll(ray, Time.deltaTime * PlayerSpeed))
                {
                    if (hit.collider.gameObject == Origin)
                    {
                        //TODO Playsound cancel animation lock
                        EndProjectile();
                    }
                }
            }
        }
    }

    public void Exectute()
    {
        _isReleased = true;
        _start = transform.position;
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(MaxGrabDuration);

        EndProjectile();
    }

    void EndProjectile()
    {
        GrabAbility.EndProjectile();
        Destroy(gameObject);
    }
}

