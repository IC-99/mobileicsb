using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punteggio : MonoBehaviour
{
    public Score player;
    public double score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            this.player.addScore(this.score);
            Destroy(gameObject);          
        }
    }
}
