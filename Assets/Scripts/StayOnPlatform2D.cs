using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject movingPlatform;
    private Vector3 lastPlatformPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            movingPlatform = collision.gameObject;
            lastPlatformPosition = movingPlatform.transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            movingPlatform = null;
        }
    }

    private void FixedUpdate()
    {
        if (movingPlatform != null)
        {
            Vector3 platformDelta = movingPlatform.transform.position - lastPlatformPosition;
            // rb.velocity += new Vector2(platformDelta.x, platformDelta.y) / Time.fixedDeltaTime;
            transform.position += platformDelta;
            lastPlatformPosition = movingPlatform.transform.position;
        }
    }
}
