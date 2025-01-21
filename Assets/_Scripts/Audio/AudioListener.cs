using UnityEngine;

public class AudioListener : MonoBehaviour
{
    [SerializeField] private AudioEventChannel audioEventChannel;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioEventChannel.OnPlayAudioEvent += PlayAudioOneShot;
    }

    private void OnDisable()
    {
        audioEventChannel.OnPlayAudioEvent -= PlayAudioOneShot;
    }

    private void PlayAudioOneShot(AudioClipSO audioClip)
    {
        _audioSource.PlayOneShot(audioClip.audioClip);
    }
}