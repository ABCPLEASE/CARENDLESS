using UnityEngine;


public class Player1CarController : BaseCarController
{
    protected override void HandleInput()
    {
        float input = 0f;
        if (Input.GetKey(KeyCode.A)) input = -1f;
        else if (Input.GetKey(KeyCode.D)) input = 1f;

        horizontalVelocity += input * horizontalAcceleration * Time.deltaTime;

        if (input == 0f)
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, 0f, horizontalDamping * Time.deltaTime);

        horizontalVelocity = Mathf.Clamp(horizontalVelocity, -horizontalMaxSpeed, horizontalMaxSpeed);
    }

    protected override void Die()
    {
        gameManager.PlayerDied(PlayerID.Player1);
        Debug.Log("Player 1 died!");
        base.Die();  // Disable only after notifying the GameManager
    }
}
