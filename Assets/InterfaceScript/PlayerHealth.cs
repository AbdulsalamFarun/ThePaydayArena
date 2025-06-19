using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    // [SerializeField] private Animator animator;
    private float currentHealth;
    private bool IsInvunerable = false;

    

    public GameObject Panel;

    [SerializeField] private PlayerMovement playerMovement;

    public Slider healthSlider;

    private void Awake() 
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

    }
    

    public void SetInvunerable(bool IsInvunerable)
    {
        this.IsInvunerable = IsInvunerable;
    }

    public void TakeDamage(float amount)
    {

        if (currentHealth == 0) { return; }
        if (IsInvunerable) { return; }

        currentHealth -= amount;
        SoundManager.instance.Play("PlayerHit");


        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }


        Debug.Log("I took damage: " + currentHealth);
        if (currentHealth <= 0)
        {

            Die();
        } 
        }


    private void Die()
    {
        SoundManager.instance.Play("DeathPanal");
        Panel.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log("Player has died.");
    }


}
