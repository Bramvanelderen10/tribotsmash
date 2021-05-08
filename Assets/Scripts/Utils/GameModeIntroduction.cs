using UnityEngine;
using TMPro;

public class GameModeIntroduction : MonoBehaviour {

    public AudioClip Woosh;
    public AudioClip IntroductionSound;
    public string IntroductionText;

    private AudioSource _audio;
    private TextMeshProUGUI _text;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = IntroductionText;


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayWoosh()
    {
        _audio.Stop();
        _audio.clip = Woosh;
        _audio.Play();
    }

    public void PlayAnnouncer()
    {
        _audio.Stop();
        _audio.clip = IntroductionSound;
        _audio.Play();
    }
}
