using UnityEngine;

/// <summary>
/// Plays hovering and click sounds for UI buttons.  
/// </summary>
public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioEventChannel audioEventChannel;
    [SerializeField] private AudioClipSO buttonHoverClip;
    [SerializeField] private AudioClipSO buttonClickClip;

    // Plays the referenced hover sound. 
    public void PlayHoverSound()
    {
        audioEventChannel.PlayAudioOneShot(buttonHoverClip);
    }

    // Plays the referenced click sound. 
    public void PlayClickSound()
    {
        audioEventChannel.PlayAudioOneShot(buttonClickClip);
    }
}