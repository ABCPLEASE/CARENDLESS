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

    [Header("Audio")]
    public AudioClip hitSound; // Hit sound clip
    public AudioClip drivingSound; // Driving sound clip
    private AudioSource audioSource; // AudioSource for the car

    [Header("Volume Control")]
    [Range(0f, 1f)] public float volume = 1f; // Volume control for both sounds (range 0 to 1)

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

        // Set up the audio source
        audioSource = GetComponent<AudioSource>();

        // Play the driving sound loop when the game starts
        if (drivingSound != null && audioSource != null)
        {
            audioSource.clip = drivingSound;
            audioSource.loop = true;
            audioSource.Play();
            UpdateAudioVolume();
        }
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

        // Play hit sound when taking damage
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
            UpdateAudioVolume(); // Adjust volume when hit sound is played
        }

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

    // Method to update the audio volume based on the volume control
    private void UpdateAudioVolume()
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
