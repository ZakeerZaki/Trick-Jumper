using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;       // The ball's transform to follow
    public Vector3 offset;       // Offset from the ball's position
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement

    private void LateUpdate()
    {
        if (ball != null)
        {
            // Desired position with the ball's Z-axis position and offsets for X and Y
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, ball.position.z + offset.z);

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;

            // Keep the camera looking at the ball
            transform.LookAt(ball);
        }
    }
}
