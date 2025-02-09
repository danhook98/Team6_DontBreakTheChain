using TMPro;
using UnityEngine;

/// <summary>
/// Updates the text of a Text Mesh Pro Text component on the same game object with the tiles moved.
/// </summary>
public class TilesMoved : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;
    
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        // Subscribe to the OnCurrentTilesMovedChanged event from the referenced Player scriptable object.
        player.OnCurrentTilesMovedChanged += UpdateTilesMoved;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event. 
        player.OnCurrentTilesMovedChanged -= UpdateTilesMoved;
    }

    private void Awake()
    {
        // Get the Text Mesh Pro Text component on the same game object.
        _text = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Updates the text of the attached TMP Text component with 'Tiles moved: {tilesMoved}'.
    /// </summary>
    /// <param name="tilesMoved">The number of tiles moved.</param>
    public void UpdateTilesMoved(byte tilesMoved)
    {
        string text = $"Tiles moved: {tilesMoved}";
        _text.text = text;
    }
}
