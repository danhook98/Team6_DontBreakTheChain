using UnityEngine;
using TMPro;

public class RollValue : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;

    private Animator _animator;

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
        _animator = GetComponent<Animator>();
    }

    public void UpdateRollValue(byte roll)
    {
        _animator.Play($"Dice Roll {roll == 1 ? 2 : 1}");
        _animator.Play($"Dice Roll {roll}");
    }
}
