using UnityEngine;
using UnityEngine.UI;

// This is our new parent class. It contains all the common UI logic.
public abstract class EnemyHealthBase : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Reference to the enemy's simple health slider.")]
    public Slider healthSlider;

    // --- THE FIX IS HERE ---
    // maxHealth is now public and has a default value, so it won't be zero.
    // Child scripts will use this variable.
    [Header("Stats")]
    public float maxHealth = 100f;

    // "protected" means this variable can be seen by this class and any child class (like AxeEnemyHealth)
    protected float currentHealth;

    // We use "virtual" so child classes can add to this method if needed
    protected virtual void Awake()
    {
        // Set up the slider's initial state and hide it
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            HideHealthBar();
        }
    }

    // These methods are now available to any script that inherits from this one
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