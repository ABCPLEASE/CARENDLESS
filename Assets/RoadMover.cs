using UnityEngine;

public class RoadMover : MonoBehaviour
{
    public float resetZ = -30f;      // When the road moves beyond this point on the Z-axis (negative direction)
    public float startZ = 30f;       // Where to reposition the road along the Z-axis (positive direction)
    public Transform player;         // Reference to the player's transform
    public Player1CarController player1CarController; // Reference to Player1's CarController
    public Player2CarController player2CarController; // Reference to Player2's CarController

    void Update()
    {
        // Determine which car controller to use based on the player
        if (player1CarController == null && player2CarController == null)
        {
            Debug.LogError("Both Player1CarController and Player2CarController are not assigned!");
            return;
        }

        float speed = 0f;

        // Choose the correct car controller based on which player
        if (player1CarController != null) // Assuming Player 1's car is active
        {
            speed = player1CarController.moveSpeed;
        }
        else if (player2CarController != null) // Assuming Player 2's car is active
        {
            speed = player2CarController.moveSpeed;
        }

        // Move the road if the speed is valid
        if (speed > 0)
        {
            Vector3 movement = Vector3.back * speed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            if (transform.position.z < resetZ)
            {
                Vector3 newPos = transform.position;
                newPos.z = startZ;
                transform.position = newPos;
            }
        }
        else
        {
            Debug.LogWarning("Speed is zero or negative! Road won't move.");
        }
    }
}
