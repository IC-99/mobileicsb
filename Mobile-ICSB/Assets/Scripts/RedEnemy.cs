using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDying;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float opacity;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth;
        this.healthBar.setMaxHealth(maxHealth);
        this.isDying = false;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.opacity = 1f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isDying)
        {
            this.rb.rotation += 10f;
            this.sr.color = new Color(1f, 1f, 1f, opacity);
            opacity -= 0.03f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            takeDamage(collision.collider.GetComponent<Bullet>().getDamage());
        }

        if (collision.collider.CompareTag("EnemyStella"))
        {
            takeDamage(collision.collider.GetComponent<EnemySella>().getDamage());
        }
    }

    private void takeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.healthBar.setHealth(currentHealth);
        if(this.currentHealth <= 0) death();
    }

    private void death()
    {
        this.healthBar.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
        this.GetComponent<Collider2D>().enabled = false;
        this.isDying = true;
        Destroy(gameObject, 1f);
    }
}
