using UnityEngine;
using UnityEngine.SceneManagement;

// 1. Notice it now inherits from EnemyHealthBase instead of MonoBehaviour
public class EnemyHealth : EnemyHealthBase, IDamageable
{
    private Animator animator;
    [SerializeField] private EnemyBlock enemyBlock;

    // 2. We override the parent's Awake method
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        // 3. Set health from the parent class. This will now correctly be 100.
        currentHealth = maxHealth;

        // 4. Call the parent's Awake() method to set up the UI.
        base.Awake();
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        if (enemyBlock != null && enemyBlock.IsBlocking)
        {
            SoundManager.instance.Play("Block");
            Debug.Log("Enemy is blocking the attack.");
            return;
        }

        SoundManager.instance.Play("EnemyHit");
        animator.SetTrigger("Hit");
        currentHealth -= amount;

        // 5. Update the slider's value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        HideHealthBar(); // 6. We can call HideHealthBar() because we inherit it

        gameObject.SetActive(false);
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        SceneManager.LoadScene("WinGameScene");
    }
}