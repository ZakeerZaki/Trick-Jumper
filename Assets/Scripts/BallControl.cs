using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float jumpForce = 10f;      // The force applied when the ball jumps upward
    public float forwardForce = 5f;    // The force applied when the ball jumps forward
    public float rollTorque = 5f;      // The torque applied to make the ball roll in the air
    public float bounceFactor = 0.5f;  // The factor that determines how much the ball bounces (0.5 for half height bounces)
    public int maxBounces = 3;         // Maximum number of bounces when the ball lands
    public ScoreManager scoreManager;  // Reference to the ScoreManager script

    private Rigidbody rb;
    private bool isGrounded;
    private int bounceCount;
    private bool firstJumpCompleted = false; // Tracks whether the first jump has been completed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = rollTorque;  // Set the maximum angular velocity for the ball's rotation speed
    }

    void FixedUpdate()
    {
        // Handle mouse click or touch input for jumping
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isGrounded)
        {
            Jump();
        }

        // Prevent the ball from moving backwards on the Z-axis
        if (rb.velocity.z < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
    }

    void Jump()
    {
        if (!rb.isKinematic && isGrounded)
        {
            isGrounded = false;
            bounceCount = 0;  // Reset bounce count

            rb.velocity = Vector3.zero;

            // Apply upward and forward forces
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * forwardForce, ForceMode.Impulse);

            // Apply torque to make the ball roll in the air
            rb.AddTorque(Vector3.right * rollTorque, ForceMode.Impulse);

            firstJumpCompleted = true;  // Mark the first jump as done
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the ball lands on a surface (ground)
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            if (bounceCount < maxBounces)
            {
                // Calculate bounce height
                float bounceHeight = jumpForce * Mathf.Pow(bounceFactor, bounceCount + 1);
                rb.velocity = new Vector3(rb.velocity.x, bounceHeight, rb.velocity.z);
                bounceCount++;
            }
            else
            {
                isGrounded = true;  // Ball has stopped bouncing

                // Update the score only if it's not the first landing after the first jump
                if (scoreManager != null && firstJumpCompleted)
                {
                    scoreManager.AddScore(1);  // Add 1 point
                }

                // Allow score updates for future jumps
                firstJumpCompleted = false;
            }
        }
    }
}
