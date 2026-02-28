using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10; // 5 full hearts
    [SerializeField] private HealthVisual healthVisual;
    [SerializeField] private RespawnScript respawnScript;

    private float currentHealth;
    private bool isInv;

    private void Start()
    {
        currentHealth = maxHealth;
        healthVisual.SetupHearts(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (!isInv)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            healthVisual.SetHealth(currentHealth);
            StartCoroutine(InvencibilityFrames());
        }

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        healthVisual.SetHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died!");
        respawnScript.Respawn();
    }

    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => currentHealth;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthVisual.SetupHearts(maxHealth);
    }

    private IEnumerator InvencibilityFrames()
    {
        isInv = true;

        yield return new WaitForSeconds(5);

        isInv = false;
    }
}