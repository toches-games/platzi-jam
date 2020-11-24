﻿using UnityEngine;
using UnityEngine.Rendering;

public class DizzyEffect : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Volume volume;
    private float targetValue;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (PlayerController.SI.State == PlayerState.Normal) return;

        if(volume.weight == 1)
        {
            targetValue = 0.8f;
        }

        else if(volume.weight <= 0.8f)
        {
            targetValue = 1f;
        }

        volume.weight = Mathf.MoveTowards(volume.weight, targetValue, speed * Time.deltaTime);
    }
}