using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public RespawnScript respawn;
    public GameObject respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawnPoint.transform.position = this.transform.position;
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.Heal(health.GetMaxHealth());
            respawn.SetCheckpoint(transform.position);

            SavePrefs();

        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("PlayerX", respawn.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", respawn.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", respawn.transform.position.y);
        PlayerPrefs.Save();
    }
}
