using UnityEngine;

public class Enemies : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void takeDamage(int damage){
        currentHealth = currentHealth - damage;

        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }
}
