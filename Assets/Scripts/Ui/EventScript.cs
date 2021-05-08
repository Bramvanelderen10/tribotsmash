using UnityEngine;
using System.Collections;
using Tribot;
using UnityEngine.EventSystems;

public class EventScript : MonoBehaviour
{

    public static EventSystem Instance;
    private GameObject _storedObj;
    [SerializeField] private CustomClip _onSelected;

    private GameObject _lastSelected;
    private AudioSource _source;

	// Use this for initialization
	void Awake()
	{
        this.CheckIfAlreadyExists(true);
        DontDestroyOnLoad(this);
        Instance = GetComponent<EventSystem>();
	    _storedObj = Instance.firstSelectedGameObject;
	    _source = gameObject.AddAudioSource();

	}
	
	// Update is called once per frame
	void Update () {
	    if (Instance.currentSelectedGameObject != _storedObj)
	    {
	        if (Instance.currentSelectedGameObject == null)
	        {
                Instance.SetSelectedGameObject(_storedObj);
	        }
	        {
	            _storedObj = Instance.currentSelectedGameObject;
	        }
	    }

	    if (_lastSelected != Instance.currentSelectedGameObject)
	    {
	        _lastSelected = Instance.currentSelectedGameObject;
	        if (_onSelected != null)
	            _onSelected.Play(_source);
	    }
	}
}
