using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderValueUpdater : MonoBehaviour
{

    public TextMeshProUGUI Target;
    public float Multiplier = 1f;
    private Slider _slider;

    // Use this for initialization
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Target.text = (_slider.value * Multiplier).ToString();
    }
}
