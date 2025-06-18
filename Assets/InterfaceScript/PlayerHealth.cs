using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    // [SerializeField] private Animator animator;
    private float currentHealth;

    public GameObject Panel;

    [SerializeField] private PlayerMovement playerMovement;

    private void Awake() => currentHealth = maxHealth;

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

        Panel.SetActive(true);
        Time.timeScale = 0f;
        
        Debug.Log("Player has died.");
    }
}
