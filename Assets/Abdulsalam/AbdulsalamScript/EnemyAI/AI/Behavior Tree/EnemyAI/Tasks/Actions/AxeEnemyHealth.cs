using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AxeEnemyHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;


    public Slider healthSlider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;

            HideHealthBar();
        }
    }

    public void TakeDamage(float amount)
    {

        SoundManager.instance.Play("EnemyHit");

        animator.SetTrigger("Hit");

        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        Debug.Log("Enemy took damage: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy has died.");

        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);

        // Disable colliders to avoid being hit again
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        SceneManager.LoadScene("WinFightScene-LVL1");
    }

    public void ShowHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
        }
    }
    public void HideHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }

}
