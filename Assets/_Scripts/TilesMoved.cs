using TMPro;
using UnityEngine;

public class TilesMoved : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTilesMoved(byte tilesMoved)
    {
        string text = $"Tiles moved: {tilesMoved}";
        _text.text = text;
    }
}
