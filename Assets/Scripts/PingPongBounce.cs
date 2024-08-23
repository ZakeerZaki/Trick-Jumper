using UnityEngine;

public class PingPongBounce : MonoBehaviour
{
    public float bounceDamping = 0.8f;  // Reduces the velocity after each bounce
    public float minVelocity = 0.1f;    // Minimum velocity to stop bouncing
    public float initialForce = 5f;     // Initial force applied to the ball to start the bounce
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Debug.Log("the object will jump");
        // Apply an initial force to the ball to simulate a drop
        rb.AddForce(Vector3.down * initialForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("THE ball should enter the loop now");
        // Check if the ball is hitting the ground
        if (collision.contacts[0].normal.y > 0.5f)
        {

            //Debug.Log("jumping now");
            Vector3 velocity = rb.velocity;

            // Reflect the velocity based on the collision normal
            velocity.y = Mathf.Abs(velocity.y) * bounceDamping;

            // Apply the reflected velocity back to the Rigidbody
            rb.velocity = velocity;

            // Stop the ball if the velocity is too low
            if (rb.velocity.magnitude < minVelocity)
            {
                rb.velocity = Vector3.zero;
                //rb.isKinematic = true;
            }
        }
    }
}
