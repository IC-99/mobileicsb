using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public bool dannoAumentato = false;
    public bool freqAumentata = false;
    public bool triploProiettile = false;

    public bool getDannoAumentato()
    {
        return this.dannoAumentato;
    }

    public bool getFreqAumentata()
    {
        return this.freqAumentata;
    }

    public bool getDoppioProiettile()
    {
        return this.triploProiettile;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PowerUpDannoAumentato"))
        {
            this.dannoAumentato = true;
            Destroy(collision.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
