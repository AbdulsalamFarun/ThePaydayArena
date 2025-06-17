using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private PlayerMovement playerMovement;

    private void Aweke() => currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        if (playerMovement.IsBlocking)
        {
            Debug.Log("Player Is Blocking");
            return;
        }
        currentHealth -= amount;
        Debug.Log("I took damage: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
