using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubItem : MonoBehaviour
{
    public Vector2 Scale = new Vector3(1f, 1f);

    private RectTransform _rectTransfrom;
    private RectTransform _parentTransfrom; 
    
	// Use this for initialization
	void Start ()
	{
	    _rectTransfrom = GetComponent<RectTransform>();
        _parentTransfrom = transform.parent.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector2 size = _parentTransfrom.sizeDelta;
	    size.x *= Scale.x;
	    size.y *= Scale.y;
	    _rectTransfrom.sizeDelta = size;

	}
}
