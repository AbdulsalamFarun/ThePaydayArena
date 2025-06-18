using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private EnemyBlock enemyBlock;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (enemyBlock != null && enemyBlock.IsBlocking)
        {
            Debug.Log("Enemy is blocking the attack.");
            return;
        }

        currentHealth -= amount;
        Debug.Log("Enemy took damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");
        // Play death animation, disable enemy, etc.
    }
}
