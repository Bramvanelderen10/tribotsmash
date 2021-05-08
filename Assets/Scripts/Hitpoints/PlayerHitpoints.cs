using UnityEngine;
using Tribot;

public class PlayerHitpoints : TribotBehaviour, Hitpoints
{
    public bool DisplayHpBar = false;
    public GameObject HealthBarPrefab;

    private Target _target;
    private GameObject _hb;
    private float _hp;
    private float _maxHp;

    public bool IsAlive
    {
        get { return Hitpoints > 0f; }
    }

    public float Hitpoints
    {
        get { return _hp; }
        set { _hp = _maxHp = value; }
    }

    public float HpPercentage
    {
        get { return Hitpoints / _maxHp; }
    }

    // Use this for initialization
    void Start ()
    {
        if (DisplayHpBar && HealthBarPrefab)
        {
            _hb = Instantiate(HealthBarPrefab);
            _hb.name = "HealthBar";
            _hb.transform.parent = transform;
            _hb.SetActive(DisplayHpBar);
        }

        _target = GetComponent<Target>();
        _target.HP = this;
    }

    // Update is called once per frame
    void Update () {
	    if (!IsAlive)
	    {
            _target.Kill();
	    }
        if (_hb)
            _hb.SetActive(DisplayHpBar);
        if (DisplayHpBar)
        {
            
            _hb.GetComponent<IHealthBar>().SetHp(HpPercentage);
        }
            
	}
    
    public bool AddDamage(float damage)
    {
        if (!IsMaster)
            return IsAlive;

        photonView.RPC("Damage", PhotonTargets.All, damage);

        return IsAlive;
    }

    [PunRPC]
    void Damage(float damage)
    {
        _hp -= damage;
    }
}
