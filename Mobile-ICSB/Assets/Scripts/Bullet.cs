using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    public int damage = 20;


    //public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.8f);
    }

    // Update is called once per frame
    void Update()
    {

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
   
    if (!collision.collider.CompareTag("Bullet") && !collision.collider.CompareTag("Player"))
    {
        explode();
    }
    
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
