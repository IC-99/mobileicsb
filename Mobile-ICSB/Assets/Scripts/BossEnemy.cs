using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
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
    public Transform GfxTransform;
    private Vector2 direzioneSparo;
    private bool isChanging = false;
    private int color = 0;
    private int timer = 0;
    public EnemyAI enAI;
    public int spinHealth;
    bool isSpinning = false;

    public Transform firepointSingolo;
    public Transform firepointMultiplo;
    private int damageOnCollison = 5;
    public PlayerHealth playerHealth;
    public Score score;
    public GameObject VictoryObject;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = maxHealth;
        this.healthBar.setMaxHealth(maxHealth);
        this.isDying = false;
        this.opacity = 1f;
        this.isShooting = false;
        InvokeRepeating("ChangeColor", 0.5f, 1f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (this.currentHealth <= this.spinHealth && !isSpinning)
        {
            this.isSpinning = true;
            this.enAI.minDistance = 5;
            this.enAI.speed = 4000;
            this.enAI.canRotate = false;
            this.rb.constraints = RigidbodyConstraints2D.None;
            // this.rb.AddTorque(2000);
            this.sr.color = Color.red;
        }

        if (isSpinning)
            GfxTransform.Rotate(new Vector3(0, 0, 720 * Time.deltaTime));

        if (isDying)
        {
            this.rb.rotation += 10f;
            this.sr.color = new Color(1f, 1f, 1f, opacity);
            opacity -= 0.03f;
        }

        if (!isShooting && !isDying && !isChanging && !isSpinning)
        {
            StartCoroutine(shooting(1f));
        }
    }

    void ChangeColor()
    {
        if (!isSpinning)
        {
            this.timer += 1;
            if (this.timer == 3)
            {
                this.timer = 0;
                this.isChanging = true;
                if (this.color == 0) this.color = 1;
                else this.color = 0;
                this.animazioneCambiaColore();

            }
            if (this.timer == 0)
            {
                this.isChanging = false;
            }
        }
    }

    void animazioneCambiaColore()
    {
        if (this.color == 0) sr.color = Color.blue;
        else sr.color = Color.white;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            this.playerHealth.takeDamage(1);
            this.playerHealth.takeDamage(2 * Time.deltaTime);
        }
    }

    private void takeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.healthBar.setHealth(currentHealth);
        if (this.currentHealth <= 0) death();
    }

    private void death()
    {
        this.healtBarUI.SetActive(false);
        this.GetComponent<Collider2D>().enabled = false;
        this.isDying = true;
        Destroy(gameObject, 1f);
        this.VictoryObject.SetActive(true);
        this.score.addScore(200);
    }


    void shootSingolo()
    {
        this.direzioneSparo = new Vector2(player.position.x, player.position.y) - (Vector2)this.firepointSingolo.position;
        this.direzioneSparo = Vector2.ClampMagnitude(this.direzioneSparo, 1f);
        GameObject bullet = Instantiate(bulletPrefab, this.firepointSingolo.position, this.firepointSingolo.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direzioneSparo * 0.5f, ForceMode2D.Impulse);

        //this.ShootSound.Play();
    }

    private Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    void shootMultiplo()
    {
        this.direzioneSparo = new Vector2(player.position.x, player.position.y) - (Vector2)this.firepointMultiplo.position;
        this.direzioneSparo = Vector2.ClampMagnitude(this.direzioneSparo, 1f);

        GameObject bullet = Instantiate(bulletPrefab, this.firepointMultiplo.position, this.firepointMultiplo.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direzioneSparo * 0.5f, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPrefab, this.firepointMultiplo.position, this.firepointMultiplo.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(rotate(this.direzioneSparo, 0.4f) * 0.5f, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(bulletPrefab, this.firepointMultiplo.position, this.firepointMultiplo.rotation);
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(rotate(this.direzioneSparo, -0.4f) * 0.5f, ForceMode2D.Impulse);

        //this.ShootSound.Play();
    }

    void shoot()
    {
        if (this.color == 0)
            this.shootSingolo();

        if (this.color == 1)
            this.shootMultiplo();

    }

    public IEnumerator shooting(float time)
    {
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        shoot();
        //wait for some time
        yield return new WaitForSeconds(time);
        isShooting = false;
    }

    public int getDamage()
    {
        return this.damageOnCollison;
    }


}