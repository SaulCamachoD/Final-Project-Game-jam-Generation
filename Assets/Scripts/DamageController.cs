using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int damageBullet = 1;
    public int damageRocket = 20;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("Me dio");
            TakeDamage(damageBullet);
        }
        if (collision.gameObject.CompareTag("Rocket"))
        {
            print("Me dio");
            TakeDamage(damageRocket);
        }
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

  
    void Die()
    {
        Debug.Log("El jugador ha muerto");
      
    }
}
