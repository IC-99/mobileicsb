using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySella : MonoBehaviour
{

    private bool isDying = false;
    public Transform target;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private float opacity = 1f;
    private int damage;

    private float raggioDiAzione = 40f;
    private float distanzaPlayer;

    private bool attivato = false;
    private bool direzioneCalcolata = false;

    private Vector3 direzione;


    void Start()
    {
        this.isDying = false;
        this.damage = 30;
        this.sr = this.GetComponent<SpriteRenderer>();
        this.distanzaPlayer = Vector2.Distance(transform.position, target.position); 
    }


    private void FixedUpdate()
    {

        this.distanzaPlayer = Vector2.Distance(transform.position, target.position);

        if (this.distanzaPlayer < this.raggioDiAzione)
        {
            this.attivato = true;
            this.rb.rotation += 5f;
            if (this.distanzaPlayer > this.raggioDiAzione / 2f && !this.direzioneCalcolata) calcolaDirezione();
            if (this.distanzaPlayer < this.raggioDiAzione / 2f) this.direzioneCalcolata = true;
        }
        
        if (attivato) rb.position = new Vector2(rb.position.x + this.direzione.x * 40f * Time.deltaTime, rb.position.y + this.direzione.y * 40f * Time.deltaTime);

        if (this.isDying)
        {
            this.rb.rotation -= 10f;
            this.sr.color = new Color(1f, 1f, 1f, opacity);
            opacity -= 0.03f;
            transform.localScale = new Vector3(10 * opacity, 10 * opacity, 10 * opacity);
        }
    }

    private void calcolaDirezione()
    {
        this.direzione = new Vector2(target.position.x, target.position.y) - rb.position;
        this.direzione = Vector2.ClampMagnitude(this.direzione, 1f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        death();
    }

    public int getDamage()
    {
        return this.damage;
    }

    private void death()
    {
        this.GetComponent<Collider2D>().enabled = false;
        this.isDying = true;
        Destroy(gameObject, 1f);
    }
}
