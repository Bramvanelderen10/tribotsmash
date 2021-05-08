using System.Collections;
using System.Collections.Generic;
using Tribot;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    [SerializeField] private float _fallSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _fadeInSpeed = 1f;
    [SerializeField] private float _fadeOutSpeed = 2f;
    [SerializeField] private float _radius = 2.2f;

    [SerializeField] private GameObject _meteorPrefab;
    [SerializeField] private HitEffects _hitFx;
    [SerializeField] private Projector _marker;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f), SerializeField]
    private Color _targetRgb = new Color(5, 2.2f, 2.2f);
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private CustomClip _hitClip;
    
    private bool _isFalling = false;
    private bool _markerEnabled = false;
    private Color _color = new Color(0, 0, 0);
    private float _fadeSpeed;
    private Vector3 _rotationDirection = Vector3.zero;
    private AudioSource _audio;

    void Start()
    {
        TargetContainer.Instance.Meteors.Add(gameObject);

        _audio = GetComponent<AudioSource>();
        _fadeSpeed = _fadeInSpeed;
        _marker.material = new Material(_marker.material);
        _marker.material.color = _color;
        Fall();
    }

    void FixedUpdate()
    {
        if (_isFalling)
        {
            RaycastHit hit;
            if (Physics.Raycast(_meteorPrefab.transform.position, Vector3.down, out hit, _groundLayer))
            {
                var dist = Vector3.Distance(_meteorPrefab.transform.position, hit.point);

                if (dist < 1f)
                {
                    DestroyMeteor();
                    _fadeSpeed = _fadeOutSpeed;
                    _color = new Color(0, 0, 0);
                    return;
                }

                if (dist < 15f && dist > (_radius + 1f))
                {
                    _fadeSpeed = _fadeInSpeed;
                    _color = _targetRgb;
                }
                else
                {
                    _fadeSpeed = _fadeOutSpeed;
                    _color = new Color(0, 0, 0);
                }
            }
            


            _meteorPrefab.transform.position = Vector3.MoveTowards(_meteorPrefab.transform.position, transform.position, Time.deltaTime*_fallSpeed);
            _meteorPrefab.transform.Rotate(_rotationDirection*_rotationSpeed*Time.deltaTime);
            foreach (var item in Physics.OverlapSphere(_meteorPrefab.transform.position, _radius))
            {
                var comp = item.GetComponent<Target>();
                if (comp)
                {
                    comp.Kill();
                }
            }
        }
        _marker.material.color = Color.Lerp(_marker.material.color, _color, _fadeSpeed * Time.deltaTime);
    }

    void OnDestroy()
    {
        TargetContainer.Instance.Meteors.Remove(gameObject);
    }

    private void DestroyMeteor()
    {
        _isFalling = false;
        Destroy(_meteorPrefab);
        _hitClip.Play(_audio);
        _hitFx.Execute();
        Destroy(gameObject, 1f);
    }

    public void Fall()
    {
        _isFalling = true;
        _color = new Color(0, 0, 0);
        _marker.material.color = _color;
        _rotationDirection = (new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))).normalized;
    }
}
