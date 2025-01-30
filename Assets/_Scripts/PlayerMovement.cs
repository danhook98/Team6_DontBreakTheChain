using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data Reference")]
    [SerializeField] private PlayerSO player;
    [SerializeField] private Vector3ValueSO chainAnchorPointData;
    
    [Header("Component References")]
    [SerializeField] private Transform anchorTransform;
    
    [Header("Movement Variables")]
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float moveTime = 0.5f;
    [SerializeField] private float introMoveTime = 1.5f;
    
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

    private void FixedUpdate()
    {
        player.Position = _rigidbody.position;

        chainAnchorPointData.value = anchorTransform.position; 
    }

    private IEnumerator MovePlayer(float timeToMove)
    {
        float elapsedTime = 0f;
        _currentPosition = _rigidbody.position;

        while (elapsedTime < timeToMove)
        {
            Vector3 lerpPosition = Vector3.Lerp(_currentPosition, _goalPosition,
                movementCurve.Evaluate(elapsedTime / timeToMove));
            _rigidbody.MovePosition(lerpPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Set the player object's position to the goal position. Even though it's been lerped, the final position will 
        // be a very small distance away from the goal position. 
        _rigidbody.MovePosition(_goalPosition);
    }

    // Move overload for use by the move event.
    private void Move(byte steps) => Move(steps, moveTime);
    
    private void Move(byte steps, float time)
    {
        Vector3 movement = Vector3.forward * steps;
        _goalPosition = _rigidbody.position + movement;
        StartCoroutine(MovePlayer(time));
    }

    private void SetStartPosition(Vector3 startPosition)
    {
        _goalPosition = startPosition;
        StartCoroutine(MovePlayer(introMoveTime));
    }
}
