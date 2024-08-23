using UnityEngine;

public class BallRotationSlowDown : MonoBehaviour
{
    public float slowdownRate = 0.95f;  // Rate at which the ball's rotation slows down each frame (0.95 means it slows by 5% each frame)
    public float stopThreshold = 0.1f;  // Angular velocity threshold below which the ball's rotation is considered stopped

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gradually reduce the ball's angular velocity to slow down its rotation
        if (rb.angularVelocity.magnitude > stopThreshold)
        {
            rb.angularVelocity *= slowdownRate;
        }
        else
        {
            // If the angular velocity is below the threshold, stop the rotation completely
            rb.angularVelocity = Vector3.zero;
        }
    }
}
