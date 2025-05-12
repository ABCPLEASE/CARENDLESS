using UnityEngine;
using UnityEngine.UI;
public enum PlayerID 
{
    Player1,
    Player2
}

public abstract class BaseCarController : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 3;
    protected int currentHP;

    [Header("UI")]
    public Text hpText;
    public Text scoreText;
    public Text distanceText;
    public Text speedText;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float maxSpeed = 100f;
    public float speedGainRate = 0.05f;
    public float horizontalAcceleration = 20f;
    public float horizontalMaxSpeed = 5f;
    public float horizontalDamping = 10f;
    public float boundaryLimit = 5f;

    protected float horizontalVelocity = 0f;
    protected float distanceTraveled = 0f;
    protected float startX;

    public GameManager gameManager;

    protected abstract void HandleInput();

    protected virtual void Start()
    {
        currentHP = maxHP;
        startX = transform.position.x;
        UpdateHPUI();
    }

    protected virtual void Update()
    {
        HandleInput();
        ApplyHorizontalMovement();
        UpdateScoreAndDistance();
        IncreaseSpeedOverTime();
    }

    protected void ApplyHorizontalMovement()
    {
        float nextX = transform.position.x + horizontalVelocity * Time.deltaTime;
        float minX = startX - boundaryLimit;
        float maxX = startX + boundaryLimit;
        nextX = Mathf.Clamp(nextX, minX, maxX);
        transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
    }

    protected void UpdateScoreAndDistance()
    {
        
        distanceTraveled += Time.deltaTime * moveSpeed;
        if (distanceText != null)
            distanceText.text = "Distance: " + Mathf.FloorToInt(distanceTraveled) + " m";

        // Update speed UI
        if (speedText != null)
            speedText.text = "Speed: " + Mathf.FloorToInt(moveSpeed) + " km/h"; 
    }


    protected void IncreaseSpeedOverTime()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += speedGainRate * Time.deltaTime;
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        }
    }

    protected void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = "HP: " + currentHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        UpdateHPUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        moveSpeed = 0f;
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
