using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTextContainer : MonoBehaviour
{
    [SerializeField] private Vector3 _textOffset = new Vector3(0f, 5f, 1.3f);
    private Transform _textContainerTransform;
    private TextMeshPro _nameText;
    private Transform _target;

    /// <summary>
    /// Popup stuff
    /// </summary>
    [Header("Pop up settings")]
    [SerializeField]
    private float _duration = .5f;
    [SerializeField]
    private AnimationCurve _curve;
    private TextMeshPro _mainText;
    private TextMeshPro _subText;

    private float _alpha = 0;

    private bool _initialized = false;

    void Start()
    {
        Initialize();
    }

    bool Initialize()
    {
        if (_initialized)
            return true;

        var playerPopup = transform.Find("PlayerTextContainer");
        if (!playerPopup)
        {
            Debug.LogError("Player pop up not correctly configured");
            return false;
        }

        _target = transform;
        _textContainerTransform = playerPopup;
        _textContainerTransform.parent = null;

        var mainText = playerPopup.Find("PopupMainText");
        var subText = playerPopup.Find("PopupSubText");
        var nameText = playerPopup.Find("PlayerNameText");

        if (!mainText || !subText || !nameText)
        {
            Debug.LogError("Player pop up not correctly configured");
            return false;
        }

        _mainText = mainText.GetComponent<TextMeshPro>();
        var mainColor = _mainText.color;
        mainColor.a = 0;
        _mainText.color = mainColor;

        _subText = subText.GetComponent<TextMeshPro>();
        var subColor = _mainText.color;
        subColor.a = 0;
        _subText.color = subColor;
        _nameText = nameText.GetComponent<TextMeshPro>();

        _initialized = true;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_mainText || !_subText)
            return;
        _alpha = Mathf.MoveTowards(_alpha, 0, (1f / _duration) * Time.deltaTime);
        _mainText.color = DecreaseColor(_mainText.color);
        _subText.color = DecreaseColor(_subText.color);
    }

    void LateUpdate()
    {
        if (_textContainerTransform && _target)
        {
            _textContainerTransform.position = _target.position + _textOffset;
        }
    }

    void OnDestroy()
    {
        Destroy(_textContainerTransform.gameObject);
    }

    public void SetName(string name)
    {
        if (!Initialize())
        {
            Debug.LogError("Player text initialization failed");
            return;
        }

        if (!_nameText)
        {
            Debug.LogError("Player pop up not correctly configured");
            return;
        }

        _nameText.text = name;
    }

    public void Popup(string text)
    {
        if (!Initialize())
        {
            Debug.LogError("Player text initialization failed");
            return;
        }

        if (!_mainText || !_subText)
        {
            Debug.LogError("Player pop up not correctly configured");
            return;
        }

        _mainText.text = text;
        _subText.text = "";
        _alpha = 1;
    }

    public void Popup(string mainText, string subText)
    {
        if (!Initialize())
        {
            Debug.LogError("Player text initialization failed");
            return;
        }

        if (!_mainText || !_subText)
        {
            Debug.LogError("Player pop up not correctly configured");
            return;
        }

        _mainText.text = mainText;
        _subText.text = subText;
        _alpha = 1;
    }

    Color DecreaseColor(Color color)
    {
        if (_alpha != 0)
        {
            color.a = _curve.Evaluate(1f - _alpha);
        }
        return color;
    }
}
