using TMPro;
using UnityEngine;

public class DiceButtonPrompt : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private string playerSide;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // This feels a little sloppy, but the game is so simplistic that this will suffice.
        if (!playerSide.Equals("Left") && !playerSide.Equals("Right"))
        {
            Debug.LogWarning("DiceButtonPrompt requires player side to be 'Left' or 'Right'!");
            return;
        }
        
        SetText();
    }

    private void SetText()
    {
        string rollDiceBind = inputReader.GetBinding($"Gameplay/{playerSide}PlayerRoll");
        string swapDiceBind = inputReader.GetBinding($"Gameplay/{playerSide}PlayerSwap");
        
        string text = $"Roll dice: {rollDiceBind}\nSwap dice: {swapDiceBind}";
        _text.text = text;
    }
}
