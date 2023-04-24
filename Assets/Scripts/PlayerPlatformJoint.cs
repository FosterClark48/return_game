using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformJoint : MonoBehaviour
{
    private FixedJoint2D joint;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && IsOnTopOfPlatform(collision) && joint == null)
        {
            Rigidbody2D platformRigidbody = collision.rigidbody;
            joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = platformRigidbody;
            joint.enableCollision = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }

    private bool IsOnTopOfPlatform(Collision2D collision)
    {
        Vector3 collisionNormal = collision.GetContact(0).normal;
        return collisionNormal.y < -0.7f;
    }
}
