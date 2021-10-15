using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootJoystick : MonoBehaviour
{
    public ShootJoystickIsUsing isUsing;

    public Transform firePoint;
    public GameObject bulletPrefab;

    protected bool isShooting;

    public float bulletForce = 20f;

    public Transform player;
    private bool touchStart = false;

    private Vector2 pointA;

    public RectTransform shootKnob;
    public RectTransform shootOuter;

    public Rigidbody2D rb;


    void Start()
    {
        isUsing = FindObjectOfType<ShootJoystickIsUsing>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Input.mousePosition;
        }
        if (isUsing.Pressed)
        {
            touchStart = true;
        }
        else
        {
            touchStart = false;
            shootKnob.position = shootOuter.position;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset / 1500, 0.1f);
            rotateCharacter(direction);

            shootKnob.position = new Vector2(shootOuter.position.x + direction.x, shootOuter.position.y + direction.y);
        }

        if (touchStart)
        {
            if (!isShooting)
            {
                StartCoroutine(shooting(0.4f));
            }

        }

    }
    void rotateCharacter(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public IEnumerator shooting(float time)
    {
        //Instantiate your projectile
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        shoot();
        //wait for some time
        yield return new WaitForSeconds(time);
        isShooting = false;
        
    }


}
