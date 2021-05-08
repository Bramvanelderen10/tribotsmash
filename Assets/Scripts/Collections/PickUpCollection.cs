using UnityEngine;
using System.Collections.Generic;
using Tribot;

public class PickUpCollection : CollectionManager<GameObject> 
{
    public static PickUpCollection Instance;
    public List<GameObject> PickupPrefabs;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    void Start()
    {
        AddCollection(PickupPrefabs);
    }
}
