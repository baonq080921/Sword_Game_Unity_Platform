using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [Header("Variable stuff")]
    public float currentHealth;
    public float maxHealth = 100f;

    public float chipSpeed = 2f; // Speed of back bar catching up
    public float delayTime = 0.5f; // Delay before back bar starts moving

    private float delayTimer = 0f; // Timer to track delay

    public Image frontHealthBar;
    public Image backHealthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float healthFraction = currentHealth / maxHealth;
        
        // Update front health bar immediately
        frontHealthBar.fillAmount = healthFraction;

        // If losing health, start delay timer
        if (backHealthBar.fillAmount > healthFraction)
        {
            delayTimer += Time.deltaTime;

            // After delay, make back health bar catch up smoothly
            if (delayTimer >= delayTime)
            {
                backHealthBar.fillAmount = Mathf.Lerp(backHealthBar.fillAmount, healthFraction, Time.deltaTime * chipSpeed);
            }
        }
        else
        {
            // Reset delay timer if not losing health
            delayTimer = 0f;
            backHealthBar.fillAmount = healthFraction;
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
           Destroy(gameObject);

        }
    }
}
