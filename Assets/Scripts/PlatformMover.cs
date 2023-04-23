using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlatformMover : MonoBehaviour
{
    /// <summary>The objects initial position.</summary>
    private Vector2 startPosition;
    /// <summary>The objects updated position for the next frame.</summary>
    private Vector2 newPosition;

    /// <summary>The speed at which the object moves.</summary>
    [SerializeField] private float speed = 1.8f;
    /// <summary>The maximum distance the object may move in either y direction.</summary>
    [SerializeField] private float maxDistance = 2f;


    void Start()
    {
        startPosition = transform.position;
        newPosition = transform.position;
    }

    void Update()
    {
        newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time - 2 * speed));
        transform.position = newPosition;
    }
}

