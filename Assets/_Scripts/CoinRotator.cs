using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Rotates and bobs a coin.
/// </summary>
public class CoinRotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private float bobSpeed = 1.5f;
    [SerializeField] private float bobAmplitude = 0.25f;

    private Transform _transform;
    private Vector3 _startPosition;
    private float _offset;

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
        
        // This offsets the start point for the vertical bob effect.
        _offset = Random.Range(-2f, 3f);
        
        // Rotate the coin by a random offset, this makes groups of coins look more natural.
        _transform.Rotate(Random.Range(0f, 180f) * Vector3.up);
    }

    private void Update()
    {
        _transform.Rotate((Time.deltaTime * rotateSpeed) * Vector3.up);
        _transform.position = _startPosition + (Mathf.Sin((Time.time + _offset) * bobSpeed) * bobAmplitude) * Vector3.up;
    }
}
