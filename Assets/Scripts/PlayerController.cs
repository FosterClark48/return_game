using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;
    // variable that checks if feet are touching ground
    public Transform groundCheck;
    // Used to draw a circle around players feet to check if its overlapping anything considered ground
    public float groundCheckRadius;
    // use layers to specify what is ground
    public LayerMask groundLayer;
    // Check whether player is touching ground or not
    private bool isTouchingGround;

    private Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find position of groundCheck (feet), use the radius to draw circle, and check if its overlapping any layer on ground layer
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        Debug.Log(direction);
        // If direction is right
        if(direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.73483f, 0.73483f);
        }
        // If direction is left
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.73483f, 0.73483f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
        // If jump button is pressed & isTouchingGround True
        if(Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        // Get velocity of player on x axis - mathf.abs makes whatever value a positive (e.g. moving left=negative)
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }
}
