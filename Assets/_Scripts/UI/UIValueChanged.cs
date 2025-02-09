using TMPro;
using UnityEngine;

public class UIValueChanged : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void UpdateDisplay(int value)
    {
        _text.text = $"{value}";
    }

    public void UpdateDisplay(float value)
    {
        _text.text = $"{value}";
    }
}
