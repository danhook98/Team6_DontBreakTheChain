using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioEventChannel audioEventChannel;
    [SerializeField] private AudioClipSO buttonHoverClip;
    [SerializeField] private AudioClipSO buttonClickClip;

    public void PlayHoverSound()
    {
        audioEventChannel.PlayAudioOneShot(buttonHoverClip);
    }

    public void PlayClickSound()
    {
        audioEventChannel.PlayAudioOneShot(buttonClickClip);
    }
}