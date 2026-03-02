using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool doesDamage;
    [SerializeField] private float damageAmount;

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth = - damage;
        }

        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("EnemyDied");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && doesDamage)
        {
            Debug.Log("Damage Done");

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }

}
