using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can activate random or selected children of an parent object
/// </summary>
public class SubObjectActivator : MonoBehaviour
{
    private List<GameObject> _subObjects;

	void Start ()
	{
	    _subObjects = new List<GameObject>();
	    for (int i = 0; i < transform.childCount; i++)
	    {
	        _subObjects.Add(transform.GetChild(i).gameObject);
	    }
	    ActivateRandomSub();
	}

    /// <summary>
    /// Deactivates all subobjects
    /// </summary>
    private void DeactivateSubObjects()
    {
        foreach (var sub in _subObjects)
        {
            sub.SetActive(false);
        }
    }

    /// <summary>
    /// Activates subobject based on random integer
    /// </summary>
    public void ActivateRandomSub()
    {
        DeactivateSubObjects();
        if (_subObjects == null || _subObjects.Count == 0)
            return;

        _subObjects[Random.Range(0, _subObjects.Count)].SetActive(true);
    }

    /// <summary>
    /// Activates subobject based on index
    /// </summary>
    /// <param name="index"></param>
    public void ActivateSub(int index)
    {
        if (_subObjects == null || index >= _subObjects.Count)
            return;

        DeactivateSubObjects();
        _subObjects[index].SetActive(true);
    }

    /// <summary>
    /// Adds an object as child and tracks it
    /// </summary>
    /// <param name="obj"></param>
    public void AddSub(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.SetActive(false);
        _subObjects.Add(obj);
    }
}
