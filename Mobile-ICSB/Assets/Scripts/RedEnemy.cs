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

    public GameObject healtBarUI;
    public HealthBar healthBar;

    public GameObject bulletPrefab;
    protected bool isShooting = false;
    public Transform player;
    private Vector2 direzioneSparo;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth;
        this.healthBar.setMaxHealth(maxHealth);
        this.isDying = false;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.opacity = 1f;
        this.isShooting = false;
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

        if (!isShooting && !isDying)
        {
            StartCoroutine(shooting(1f));
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
        this.healtBarUI.SetActive(false);
        this.GetComponent<Collider2D>().enabled = false;
        this.isDying = true;
        Destroy(gameObject, 1f);
    }


    void shootSingolo()
    {
        this.direzioneSparo = new Vector2(player.position.x, player.position.y) - this.rb.position;
        this.direzioneSparo = Vector2.ClampMagnitude(this.direzioneSparo, 1f);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direzioneSparo * 0.5f, ForceMode2D.Impulse);

        //this.ShootSound.Play();
    }

    public IEnumerator shooting(float time)
    {
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        shootSingolo();
        //wait for some time
        yield return new WaitForSeconds(time);
        isShooting = false;
    }
}
