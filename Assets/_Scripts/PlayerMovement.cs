using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;
    
    [Header("Movement Variables")]
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float moveTime = 0.5f;
    
    private Rigidbody _rigidbody;

    private Vector3 _currentPosition;
    private Vector3 _goalPosition;

    private void OnEnable()
    {
        player.OnMove += Move;
        player.OnSetStartPosition += SetStartPosition;
    }

    private void OnDisable()
    {
        player.OnMove -= Move;
        player.OnSetStartPosition -= SetStartPosition;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator MovePlayer()
    {
        float elapsedTime = 0f;
        _currentPosition = _rigidbody.position;

        while (elapsedTime < moveTime)
        {
            _rigidbody.position = Vector3.Lerp(_currentPosition, _goalPosition, movementCurve.Evaluate(elapsedTime / moveTime ) );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Set the player object's position to the goal position. Even though it's been lerped, the final position will 
        // be a very small distance away from the goal position. 
        _rigidbody.position = _goalPosition;
    }
    
    private void Move(byte steps)
    {
        Vector3 movement = Vector3.forward * steps;
        _goalPosition = _rigidbody.position + movement;
        StartCoroutine(MovePlayer());
    }

    private void SetStartPosition(Vector3 startPosition)
    {
        Debug.Log(startPosition);
        _rigidbody.MovePosition(startPosition);
    }
}
