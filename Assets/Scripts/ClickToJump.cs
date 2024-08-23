using UnityEngine;

public class ClickToJump : MonoBehaviour
{
    public float jumpForce = 10f;    // The force applied when the ball jumps upward
    public float forwardForce = 5f;  // The force applied when the ball jumps forward
    public ScoreManager scoreManager; // Reference to the ScoreManager script

    private Rigidbody rb;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detect input for jumping (click or tap)
        if (canJump && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!rb.isKinematic)
        {
            rb.velocity = Vector3.zero;

            // Apply upward and forward forces
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * forwardForce, ForceMode.Impulse);

            // Update the score
            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Add 1 point per jump
            }

            // Prevent further jumps while in the air
            canJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Allow jumping again when the ball touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
