using UnityEngine;
using System.Collections.Generic;

public enum AbilityTypes
{
    Bomb,
    ChargedShot,
    Dash,
    Grab,
    Punch,
    RangedGrab,
    Shield,
    Shockwave,
    Wall
}

[CreateAssetMenu(fileName = "AbilityData", menuName = "Data/AbilityData")]
public class AbilityData : ScriptableObject
{
    

    [System.Serializable]
    public class AbilityMap
    {
        public bool Enabled;
        public GameObject Prefab;
        public AbilityTypes Ability;
    }

    [Header("Prefabs")] public List<AbilityMap> _abilities = new List<AbilityMap>()
    {
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Bomb},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.ChargedShot},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Dash},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Grab},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Punch},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.RangedGrab},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Shield},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Shockwave},
        new AbilityMap() {Enabled = false, Prefab = null, Ability = AbilityTypes.Wall},

    };


    public GameObject GetAbilityPrefab(AbilityTypes ability)
    {
        var result = _abilities.Find(x => x.Ability == ability);

        if (result != null && result.Enabled)
        {
            return result.Prefab;
        }
        else
        {
            return null;
        }
    }
}