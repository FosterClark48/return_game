using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowPlatform : MonoBehaviour
{
    private Transform platform;
    private Vector3 offset;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && IsOnTopOfPlatform(collision))
        {
            platform = collision.transform;
            offset = transform.position - platform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platform = null;
        }
    }

    private void FixedUpdate()
    {
        if (platform != null)
        {
            Vector3 targetPosition = platform.position + offset;
            Vector3 velocity = (targetPosition - transform.position) / Time.fixedDeltaTime;
            playerRigidbody.velocity = new Vector2(velocity.x, playerRigidbody.velocity.y);
        }
    }

    private bool IsOnTopOfPlatform(Collision2D collision)
    {
        Vector3 collisionNormal = collision.GetContact(0).normal;
        return collisionNormal.y < -0.7f;
    }
}
