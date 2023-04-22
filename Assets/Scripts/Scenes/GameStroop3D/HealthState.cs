using UnityEngine;
using System.Collections;

public class HealthState : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    public bool canMove;

    

    void Start()
    {
        canMove = true;
        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        
    }

    public void RelivePlayer()
    {
        canMove = true;        
        healthBar.SetMaxHealth(4);
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

    }


    public void WrongDiamants()
    {
        Debug.Log("wrong diamants");
        currentHealth--;
        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {

            canMove = false;
            
        }
    }

    public void ResetHealth()
    {
        RelivePlayer();
    }

}
