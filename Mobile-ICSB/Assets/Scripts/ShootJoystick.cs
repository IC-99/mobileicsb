using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootJoystick : MonoBehaviour
{
    public ShootJoystickIsUsing isUsing;

    public Transform firePoint;
    public GameObject bulletPrefab;

    protected bool isShooting;

    public float bulletForce = 80f;

    public Transform player;
    private bool touchStart = false;

    private Vector2 pointA;

    public RectTransform shootKnob;
    public RectTransform shootOuter;

    public Rigidbody2D rb;

    public bool inuso = false;
    public Touch touch;

    public float frequency = 0.2f;

    public PlayerPowerUp powerUps;

    public AudioSource ShootSound;

    private bool isFreq;


    void Start()
    {
        isUsing = FindObjectOfType<ShootJoystickIsUsing>();
        isFreq = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (!inuso)
            {
                pointA = touch.position;
                inuso = true;
            }
        }

        if (Input.touchCount == 2)
        {

            touch = touchDestro(Input.GetTouch(0), Input.GetTouch(1));
            if (!inuso)
            {
                pointA = touch.position;
                inuso = true;
            }
        }
        if (isUsing.Pressed)
        {
            touchStart = true;
        }
        else
        {
            inuso = false;
            touchStart = false;
            shootKnob.position = shootOuter.position;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = new Vector2(touch.position.x, touch.position.y) - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset / 1500, 0.1f);
            rotateCharacter(direction);

            shootKnob.position = new Vector2(shootOuter.position.x + direction.x * 10, shootOuter.position.y + direction.y * 10);
        }

        if (touchStart)
        {
            if (!isShooting)
            {
                StartCoroutine(shooting(this.frequency));
            }

        }

    }

    void rotateCharacter(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public Touch touchDestro(Touch t1, Touch t2)
    {
        if (t1.position.x > t2.position.x)
        {
            return t1;
        }
        return t2;

    }

    void shootSingolo()
    {
        
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (this.powerUps.getDannoAumentato())
            {
                Bullet b = bullet.GetComponent<Bullet>();
                b.setDamage(b.getDamage() * 2);
            }
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        
          
        this.ShootSound.Play();
    }

    public IEnumerator shootTriplo()
    {

    GameObject b1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb1 = b1.GetComponent<Rigidbody2D>();
    rb1.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    yield return new WaitForSeconds(0.02f);

    GameObject b2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb2 = b2.GetComponent<Rigidbody2D>();
    rb2.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    yield return new WaitForSeconds(0.02f);

    GameObject b3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb3 = b3.GetComponent<Rigidbody2D>();
    rb3.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

    if (this.powerUps.getDannoAumentato())
    {
        Bullet bullet1 = b1.GetComponent<Bullet>();
        bullet1.setDamage(bullet1.getDamage() * 2);
        Bullet bullet2 = b1.GetComponent<Bullet>();
        bullet1.setDamage(bullet2.getDamage() * 2);
        Bullet bullet3 = b1.GetComponent<Bullet>();
        bullet1.setDamage(bullet3.getDamage() * 2);
    }

    this.ShootSound.Play();

    }

    public IEnumerator shooting(float time)
    {
        //Instantiate your projectile
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        if (!this.powerUps.getTriploProiettile())
        {
            shootSingolo();
        }
        else
        {
            StartCoroutine(shootTriplo());
        }
        //wait for some time
        yield return new WaitForSeconds(time);
        isShooting = false;
        
    }

    public bool getIsFreq()
    {
        return isFreq;
    }

    public void setIsFreq()
    {
        isFreq = true;
        frequency = 0.15f;
        
    }


}
