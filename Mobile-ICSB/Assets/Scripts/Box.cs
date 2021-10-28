using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private int state = 3;
    public GameObject boxExplosion;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().Play("NormalBox");
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
                Destroy(gameObject);
                Destroy(effect, 2f);
            }
        }
    }
}
