﻿using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    [SerializeField] private float initialRotationSpeed = 1f;
    [SerializeField] private float rotateTime = 5f;
    [SerializeField] private float rotateDurationTime = 1f;
    [SerializeField] private int rotationDegrees = 90;

    private Rigidbody2D rig;
    private float currentSpeed;
    private float targetSpeed;
    private float currentVelocity;
    private int[] directions = { -1, 1 };
    [SerializeField] private int direction;
    [SerializeField] bool randomDirection;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        while (GameManager.SI.currentGameState == GameState.InGame)
        {
            targetSpeed = initialRotationSpeed;
            yield return new WaitForSeconds(rotateTime);
            if (randomDirection)
            {
                direction = directions[Random.Range(0, directions.Length)];
            }
            targetSpeed = (rig.rotation + rotationDegrees - rig.rotation) * rotateDurationTime * direction;
            yield return new WaitForSeconds(rotateDurationTime);
        }
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, 0.5f);
    }

    private void FixedUpdate()
    {
        if(GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }
        rig.MoveRotation(rig.rotation - currentSpeed * Time.deltaTime);
    }
}
