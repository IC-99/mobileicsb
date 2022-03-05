using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 20;

    public Rigidbody2D rb;

    //public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.2f);
    }

    private void FixedUpdate()
    {
        this.rb.rotation += 10f;
    }


    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getDamage()
    {
        return this.damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explode();
    }


    void explode()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        //cameraShake.shaking();
    }
}
