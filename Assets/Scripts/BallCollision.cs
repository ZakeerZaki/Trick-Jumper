using UnityEngine;
using TMPro;  // Make sure to add the TextMeshPro namespace

public class BallCollision : MonoBehaviour
{
    public TextMeshProUGUI messageText;   // Reference to the TextMeshProUGUI component
    public Transform resetPosition;       // Position to reset the ball to
    public float displayDuration = 2f;    // Duration to display the message
    public ScoreManager scoreManager;     // Reference to the ScoreManager script

    private void Start()
    {
        // Hide the message text at the start
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision started");

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("collision entered");
            // Display the message
            if (messageText != null)
            {
                Debug.Log("will display the message");
                messageText.gameObject.SetActive(true);
                messageText.text = "Hit an obstacle!";
                Invoke("HideMessage", displayDuration);
            }

            // Reset the ball's position
            transform.position = resetPosition.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            // Reset the score to 0
            if (scoreManager != null)
            {
                scoreManager.ResetScore();
            }
        }
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
}
