using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(fileName = "Audio_Event_Channel", menuName = "ChainGame/Audio/Audio Event Channel")]
public class AudioEventChannel : ScriptableObject
{
    public event UnityAction<AudioClipSO> OnPlayAudioEvent; 
    
    public void PlayAudioOneShot(AudioClipSO audioClip) => OnPlayAudioEvent?.Invoke(audioClip);
}
