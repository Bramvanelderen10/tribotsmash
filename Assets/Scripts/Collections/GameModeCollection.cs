using UnityEngine;
using System.Collections.Generic;

public class GameModeCollection : CollectionManager<GameObject> {

    public GameObject GetGameModePrefab(System.Type Type)
    {
        GameObject result = null;
        foreach (var item in Collection)
        {
            if (item.Object.GetComponent(Type))
            {
                result = item.Object;
            }
        }

        return result;
    }
}
