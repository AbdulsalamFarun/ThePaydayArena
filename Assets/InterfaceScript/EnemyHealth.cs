using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;

    [SerializeField] private EnemyBlock enemyBlock;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (enemyBlock != null && enemyBlock.IsBlocking)
        {
            SoundManager.instance.Play("Block"); 
            Debug.Log("Enemy is blocking the attack.");
            return;
        }
        SoundManager.instance.Play("EnemyHit");
        animator.SetTrigger("Hit");

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
