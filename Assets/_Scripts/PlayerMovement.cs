using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float moveTime = 0.5f;
    
    private Transform _playerTransform;

    private Vector3 _currentPosition;
    private Vector3 _goalPosition;

    private void Start()
    {
        _playerTransform = GetComponent<Transform>();
    }

    private IEnumerator MovePlayer()
    {
        float elapsedTime = 0f;
        _currentPosition = _playerTransform.position;

        while (elapsedTime < moveTime)
        {
            _playerTransform.position = Vector3.Lerp(_currentPosition, _goalPosition, movementCurve.Evaluate(elapsedTime / moveTime ) );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Set the player object's position to the goal position. Even though it's been lerped, the final position will 
        // be a very small distance away from the goal position. 
        _playerTransform.position = _goalPosition;
    }
    
    public void Move(Vector3 movement)
    {
        _goalPosition = _playerTransform.position + movement;
        StartCoroutine(MovePlayer());
    }
}
