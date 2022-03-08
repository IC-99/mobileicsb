using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public AudioSource dannoAumentatoSound;
    public Timer dannoAumentatoTimer;
    public Timer triploProiettileTimer;
    public AudioSource curaSound;
    
    private bool dannoAumentato = false;
    private bool freqAumentata = false;
    private bool triploProiettile = false;

    public bool getDannoAumentato()
    {
        return this.dannoAumentato;
    }

    public void setDannoAumentato(bool b)
    {
        this.dannoAumentato = b;
    }

    public bool getFreqAumentata()
    {
        return this.freqAumentata;
    }

    public void setFreqAumentata(bool b)
    {
        this.freqAumentata = b;   
    }

    public bool getTriploProiettile()
    {
        return this.triploProiettile;
    }

    public void setTriploProiettile(bool b)
    {
        this.triploProiettile = b;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PowerUpDannoAumentato"))
        {
            this.dannoAumentato = true;
            this.dannoAumentatoTimer.startTimer(10f, "DannoAumentato");
            this.dannoAumentatoSound.Play();
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("PowerUpCura"))
        {
            playerHealth.cura(20);
            this.curaSound.Play();
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("PowerUpTriploProiettile"))
        {
            this.triploProiettile = true;
            this.triploProiettileTimer.startTimer(10f, "TriploProiettile");
            //this.triploProiettileSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
