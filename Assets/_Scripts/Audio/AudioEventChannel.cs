using UnityEngine;
using UnityEngine.Events; 

public class AudioEventChannel : MonoBehaviour
{
    public event UnityAction<AudioClipSO> OnPlayAudioEvent; 
    
    public void PlayAudioOneShot(AudioClipSO audioClip) => OnPlayAudioEvent?.Invoke(audioClip);
}
