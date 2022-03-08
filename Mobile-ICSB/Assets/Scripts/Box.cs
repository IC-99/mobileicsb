using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private int state = 3;
    public GameObject boxExplosion;
    private GameObject[] powerUps;
    public GameObject powerUpDannoAumentato;
    public GameObject powerUpCura;
    public GameObject powerUpTriploProiettile;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().Play("NormalBox");
        this.powerUps = new GameObject[3];
        this.powerUps[0] = this.powerUpDannoAumentato;
        this.powerUps[1] = this.powerUpCura;
        this.powerUps[2] = this.powerUpTriploProiettile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            state--;
            if(state == 2) gameObject.GetComponent<Animator>().Play("DamageBox");
            if(state == 1) gameObject.GetComponent<Animator>().Play("MoreDamageBox");
            if(state == 0)
            {
                GameObject effect = Instantiate(boxExplosion, transform.position, Quaternion.identity);
                Instantiate(this.powerUps[this.estrazioneCasuale(this.powerUps.Length)], transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(effect, 2f);
            }
        }
    }

    private int estrazioneCasuale(int max)
    {
        return (int)(Random.value * max);
    }
}
