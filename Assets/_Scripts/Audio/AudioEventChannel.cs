using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(fileName = "Audio_Event_Channel", menuName = "ChainGame/Audio/Audio Event Channel")]
public class AudioEventChannel : ScriptableObject
{
    public event UnityAction<AudioClipSO> OnPlayAudioOneShotEvent;
    public event UnityAction<AudioClipSO, float> OnPlayAudioEvent;
    
    public void PlayAudioOneShot(AudioClipSO audioClip) => OnPlayAudioOneShotEvent?.Invoke(audioClip);
    public void PlayAudio(AudioClipSO audioClip, float volume) => OnPlayAudioEvent?.Invoke(audioClip, volume);
}
