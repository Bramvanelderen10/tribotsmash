using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInjectAbilities : MonoBehaviour
{
    public AbilityTypes AbilityShockwave;
    public AbilityTypes AbilityRangedGrab;
    public AbilityTypes AbilityWall;
    public AbilityTypes AbilityChargedShot;
    public AbilityTypes AbilityBomb;

    private Player _player;

    // Use this for initialization
    void Start ()
    {
        _player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        _player.AddAbility(AbilityShockwave);
	    }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _player.AddAbility(AbilityRangedGrab);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.AddAbility(AbilityWall);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.AddAbility(AbilityChargedShot);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _player.AddAbility(AbilityBomb);
        }
    }
}
