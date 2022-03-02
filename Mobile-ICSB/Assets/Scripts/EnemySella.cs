using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySella : MonoBehaviour
{
    private bool attivo = false;
    public Transform target;
    public Rigidbody2D rb;
    private int damage;



    void Start()
    {
        this.attivo = true;
        this.damage = 30; 
    }


    private void FixedUpdate()
    {
        if (this.attivo)
        {
            rb.rotation = rb.rotation + 4f;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public int getDamage()
    {
        return this.damage;
    }
}
