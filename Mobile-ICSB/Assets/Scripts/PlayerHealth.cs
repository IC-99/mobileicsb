using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        this.healthBar.setMaxHealth(this.maxHealth);
        this.currentHealth = 50;
        this.healthBar.setHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            //takeDamage(collision.collider.GetComponent<EnemyBullet>().getDamage());
        }
    }

    public void cura(int valoreCura)
    {
        if(valoreCura + this.currentHealth > maxHealth)
        {
            this.currentHealth = this.maxHealth;
        }
        else
        {
            this.currentHealth += valoreCura;
        }
        this.healthBar.setHealth(currentHealth);
    }

    public void takeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.healthBar.setHealth(currentHealth);
    }
}
