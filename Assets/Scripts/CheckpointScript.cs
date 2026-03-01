using Unity.VisualScripting;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public RespawnScript respawn;
    public GameObject respawnPoint;
    public Sprite flagUp;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = flagUp;
            respawnPoint.transform.position = this.transform.position;
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.Heal(health.GetMaxHealth());
            respawn.SetCheckpoint(transform.position);
        }
    }


}
