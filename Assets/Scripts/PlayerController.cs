using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isFloating = false;
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

    private Vector3 respawnPoint;

    private CameraController cc;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;

        cc = Camera.main.gameObject.GetComponent<CameraController>();
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

        if (isFloating)
        {
            // Keep floating upwards until the player hits the ground
            if (!isTouchingGround)
            {
                player.velocity = new Vector2(player.velocity.x, Mathf.Abs(jumpSpeed));
            }
            else
            {
                // Reset gravity scale and flag when the player hits the ground
                player.gravityScale = 1f;
                isFloating = false;
            }
        }

        // Get velocity of player on x axis - mathf.abs makes whatever value a positive (e.g. moving left=negative)
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
    }

    [ContextMenu("Respawn")]
    void Respawn(){
        player.velocity = Vector2.zero;
            player.transform.position = respawnPoint;

            var newCameraPos = cc.gameObject.transform.position;

            newCameraPos = new Vector3(newCameraPos.x, player.transform.position.y, newCameraPos.z);

            cc.gameObject.transform.position = newCameraPos;
    }

    [ContextMenu("Set Spawn")]
    void SetSpawn(){
        respawnPoint = this.transform.position;
    }

    [ContextMenu("Player Float")]
    void PlayerFloat(){
        // Set player's y-velocity to a positive value to make it float upwards
        player.velocity = new Vector2(player.velocity.x, Mathf.Abs(jumpSpeed));

        // Set gravity scale to 0 to make the player float instead of falling
        player.gravityScale = 0f;

        // Set a flag to indicate that the player is floating
        isFloating = true;
    }

    public void EndGame()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spawn"))
        {
            SetSpawn();
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
            Respawn();
        }
        if (other.gameObject.CompareTag("Float"))
        {
            PlayerFloat();
        }
        if (other.gameObject.CompareTag("End"))
        {
            EndGame();
        }
    }
}
