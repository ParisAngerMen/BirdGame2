using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private HealthVisual healthVisual;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 1.5f;
    [SerializeField] private Vector2 defaultSpawnPoint;

    private Vector2 currentCheckpoint;

    private void Awake()
    {
        // Load saved checkpoint or use default
        if (PlayerPrefs.GetInt("HasSave", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("CheckpointX", defaultSpawnPoint.x);
            float y = PlayerPrefs.GetFloat("CheckpointY", defaultSpawnPoint.y);
            currentCheckpoint = new Vector2(x, y);

            // Restore saved coins
            int savedCoins = PlayerPrefs.GetInt("Coins", 0);
            GameManager.instance.SetPoints(savedCoins);
        }
        else
        {
            currentCheckpoint = defaultSpawnPoint;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    public void SetCheckpoint(Vector2 position)
    {
        currentCheckpoint = position;
    }

    private System.Collections.IEnumerator RespawnCoroutine()
    {
        // Disable player input briefly
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        // Optional: fade out, play death animation, etc.
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Move to checkpoint
        player.transform.position = currentCheckpoint;

        // Restore health
        playerHealth.ResetHealth();
        healthVisual.SetHealth(playerHealth.GetMaxHealth());

        // Restore coins to last saved amount
        int savedCoins = PlayerPrefs.GetInt("Coins", 0);
        GameManager.instance.SetPoints(savedCoins);

        // Re-enable player
        if (sr != null) sr.enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;

        Debug.Log($"Respawned at {currentCheckpoint}");
    }
}
