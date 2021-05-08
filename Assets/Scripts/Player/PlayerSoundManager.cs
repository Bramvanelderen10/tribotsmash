using UnityEngine;
using Tribot;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public CustomClip Footstep;
    public CustomClip RobotMovement;
    public CustomClip Fall;

    private AudioSource _audio;
    private AudioSource _audio2;
    private AudioSource _audio3;

    void Start()
    {
        _audio = gameObject.AddAudioSource();
        _audio2 = gameObject.AddAudioSource();
        _audio3 = gameObject.AddAudioSource();
    }

    public void PlayFootstep()
    {
        Footstep.Play(_audio);
    }

    public void PlayMovement()
    {
        RobotMovement.Play(_audio2);
    }

    public void PlayFall()
    {
        Fall.Play(_audio3);
    }
}
