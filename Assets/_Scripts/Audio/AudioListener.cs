using UnityEngine;

public class AudioListener : MonoBehaviour
{
    [SerializeField] private AudioEventChannel audioEventChannel;
    
    private AudioSource _audioSource;
    private AudioSource _audioSourceOneShot;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSourceOneShot = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioEventChannel.OnPlayAudioOneShotEvent += PlayAudioOneShot;
        audioEventChannel.OnPlayAudioEvent += PlayAudio;
    }

    private void OnDisable()
    {
        audioEventChannel.OnPlayAudioOneShotEvent -= PlayAudioOneShot;
        audioEventChannel.OnPlayAudioEvent -= PlayAudio;
    }

    private void PlayAudioOneShot(AudioClipSO audioClip)
    {
        _audioSourceOneShot.PlayOneShot(audioClip.audioClip);
    }

    private void PlayAudio(AudioClipSO audioClip, float volume = 0.5f)
    {
        _audioSource.volume = volume;
        _audioSource.clip = audioClip.audioClip;
        _audioSource.Play();
    }
}