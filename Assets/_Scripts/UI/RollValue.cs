using UnityEngine;
using TMPro;

public class RollValue : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;
    
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        player.OnCurrentRollChanged += UpdateRollValue; 
    }

    private void OnDisable()
    {
        player.OnCurrentRollChanged -= UpdateRollValue;
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateRollValue(byte roll)
    {
        string text = $"You rolled a {roll}!";
        _text.text = text;
    }
}
