using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10; // 5 full hearts
    [SerializeField] private HealthVisual healthVisual;
    [SerializeField] private RespawnScript respawnScript;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthVisual.SetupHearts(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        healthVisual.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        healthVisual.SetHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died!");
        respawnScript.Respawn();
    }

    public int GetMaxHealth() => maxHealth;
    public int GetCurrentHealth() => currentHealth;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthVisual.SetupHearts(maxHealth);
    }
}