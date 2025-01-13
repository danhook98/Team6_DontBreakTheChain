using System;
using TMPro;
using UnityEngine;

public class TilesMoved : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;
    
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        player.OnCurrentTilesMovedChanged += UpdateTilesMoved;
    }

    private void OnDisable()
    {
        player.OnCurrentTilesMovedChanged -= UpdateTilesMoved;
    }

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
