using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    private Vector2 _position;
    
    private PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _position = transform.position;
    }
    
    private void FixedUpdate()
    {
        var distance = Vector2.Distance(transform.position, _player.transform.position);
        distance = Mathf.Clamp(distance, minSize, maxSize);
        var newSize = maxSize / distance;
        transform.localScale = new Vector3(newSize, newSize, newSize);
        _position = new Vector2(_player.transform.position.x, _position.y);
    }
}
