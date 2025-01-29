using UnityEngine;
using TMPro;

public class RollValue : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;

    [SerializeField] Animator animator;

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

        // Play the Correct Animation
        
        if (roll == 1)
        {
            animator.Play("Dice Roll 2");
            animator.Play("Dice Roll 1");
        }
        if (roll == 2)
        {
            animator.Play("Dice Roll 1");
            animator.Play("Dice Roll 2");
        }
        if (roll == 3)
        {
            animator.Play("Dice Roll 1");
            animator.Play("Dice Roll 3");
        }
        if (roll == 4)
        {
            animator.Play("Dice Roll 1");
            animator.Play("Dice Roll 4");
        }
        if (roll == 5)
        {
            animator.Play("Dice Roll 1");
            animator.Play("Dice Roll 5");
        }
        if (roll == 6)
        {
            animator.Play("Dice Roll 1");
            animator.Play("Dice Roll 6");
        }
    }
}
