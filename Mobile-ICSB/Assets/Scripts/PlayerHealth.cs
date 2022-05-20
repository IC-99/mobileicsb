using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathScreen;
    private bool hasHelmet;
    public SpriteRenderer player;
    public Sprite playerConElmo;
    //public Rigidbody2d rb;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        this.healthBar.setMaxHealth(this.maxHealth);
        this.currentHealth = 100;
        this.healthBar.setHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentHealth <= 0)
        {
            Time.timeScale = 0f;
            this.deathScreen.SetActive(true);
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            takeDamage(collision.collider.GetComponent<EnemyBullet>().getDamage());
        }

        if (collision.collider.CompareTag("EnemyStella"))
        {
            takeDamage(collision.collider.GetComponent<EnemySella>().getDamage());
        }

        if (collision.collider.CompareTag("Boss"))
        {
            takeDamage(collision.collider.GetComponent<BossEnemy>().getDamage());
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
        float protezione = 1f;
        if (hasHelmet)
        {
            protezione = 0.5f;
        }
        this.currentHealth -= (int)(damage * protezione);
        this.healthBar.setHealth(currentHealth);
    }

    public bool getHasHelmet()
    {
        return hasHelmet;
    }

    public void setHelmet()
    {
        hasHelmet = true;
        player.sprite = playerConElmo;
    }

}
