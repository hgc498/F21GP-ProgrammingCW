//refrenced from: https://www.youtube.com/watch?v=e8GmfoaOB4Y
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stats damage;

    public GameObject DieUI;
    Collider enemy;

    public HealthBar healthBar;

    void Awake()
    {
        //sets the health to the max health 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //if enemy collides with player, th eplayer takes damage 
    public void OnTriggerEnter(Collider enemy)
    {
        if(enemy.gameObject.tag == "Enemy")
        {
            TakeDamage(3);
        }
    }

    //adding a take damage method 
    public void TakeDamage (int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        //updates the health bar 
        healthBar.SetHealth(currentHealth);

        //when health is 0 player will die
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //method that plays the die screen to the restart the level 
    public virtual void Die()
    {
        DieUI.SetActive(true);
    }


}
