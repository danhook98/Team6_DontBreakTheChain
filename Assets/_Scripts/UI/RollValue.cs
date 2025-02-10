using UnityEngine;
using TMPro;

/// <summary>
/// Plays dice roll animations for a dice.
/// </summary>
public class RollValue : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;

    private Animator _animator;

    private void OnEnable()
    {
        // Subscribe to the OnCurrentRollChanged event from the referenced Player scriptable object.
        player.OnCurrentRollChanged += UpdateRollValue; 
    }

    private void OnDisable()
    {
        // Unsubscribe from the event. 
        player.OnCurrentRollChanged -= UpdateRollValue;
    }

    private void Awake()
    {
        // Get the animator component on the game object.
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Plays the dice roll animation for the given roll.
    /// </summary>
    /// <param name="roll">Dice roll value between 1 and 6.</param>
    public void UpdateRollValue(byte roll)
    {
        // Play the animation for the given dice roll. 
        _animator.Play($"Dice Roll {roll}", -1, 0f);
    }
}
