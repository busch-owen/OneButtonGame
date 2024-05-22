using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float moveSpeed = -0.1f;

    [SerializeField] private float increaseRate;

    private ScoreHandler _scoreHandler;

    private void Awake()
    {
        _scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    public void SpeedupTimer()
    {
        moveSpeed *= increaseRate;
        _scoreHandler.MultiplyTickRate(increaseRate);
    }
}
