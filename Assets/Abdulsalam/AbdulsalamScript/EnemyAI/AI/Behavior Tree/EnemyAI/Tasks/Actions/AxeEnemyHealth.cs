using UnityEngine;
using UnityEngine.SceneManagement;

// 1. Notice it now inherits from EnemyHealthBase instead of MonoBehaviour
public class AxeEnemyHealth : EnemyHealthBase, IDamageable
{
    private Animator animator;

    // 2. We override the parent's Awake method to add our own logic
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        // 3. Set health from the value in the parent class. This will now correctly be 100.
        currentHealth = maxHealth;

        // 4. This calls the Awake() method in the parent (EnemyHealthBase)
        // to set up the health bar. VERY IMPORTANT!
        base.Awake();
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        SoundManager.instance.Play("EnemyHit");
        animator.SetTrigger("Hit");
        currentHealth -= amount;

        // 5. Update the slider's value (slider is inherited from the parent)
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
        Debug.Log("Enemy has died.");

        HideHealthBar(); // 6. We can call HideHealthBar() because we inherit it

        gameObject.SetActive(false);
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        SceneManager.LoadScene("WinFightScene-LVL1");
    }
}