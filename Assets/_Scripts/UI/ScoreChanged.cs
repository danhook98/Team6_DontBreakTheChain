using TMPro;
using UnityEngine;

public class ScoreChanged : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void UpdateScoreDisplay(int score)
    {
        _text.text = score.ToString();
    }
}
