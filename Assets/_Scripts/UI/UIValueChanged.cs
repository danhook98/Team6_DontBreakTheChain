using TMPro;
using UnityEngine;

/// <summary>
/// Universal script for changing the value of an attached TMP Text component. 
/// </summary>
public class UIValueChanged : MonoBehaviour
{
    private TextMeshProUGUI _text;

    // Get the TMP Text component on the same game object.
    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    /// <summary>
    /// Updates the text value of the referenced TMP Text component.
    /// </summary>
    /// <param name="value">Value to display.</param>
    public void UpdateDisplay(int value)
    {
        _text.text = $"{value}";
    }

    /// <summary>
    /// Updates the text value of the referenced TMP Text component.
    /// </summary>
    /// <param name="value">Value to display.</param>
    public void UpdateDisplay(float value)
    {
        _text.text = $"{value}";
    }
}
