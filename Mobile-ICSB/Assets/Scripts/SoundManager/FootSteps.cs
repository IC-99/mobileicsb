using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public MoveJoystick player;
    public AudioSource passo1;
    public AudioSource passo2;
    public AudioSource passo3;
    public AudioSource passo4;
    public AudioSource passo5;
    private bool inizio = false;
    private AudioSource[] passi;

    // Start is called before the first frame update
    void Start()
    {
        passi = new AudioSource[5];
        passi[0] = this.passo1;
        passi[1] = this.passo2;
        passi[2] = this.passo3;
        passi[3] = this.passo4;
        passi[4] = this.passo5;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isMoving() && !passo1.isPlaying && !passo2.isPlaying && !passo3.isPlaying && !passo4.isPlaying && !passo5.isPlaying)
        {
            passi[estrazioneCasuale(5)].Play();   
        }
    }

    private int estrazioneCasuale(int max)
    {
        return (int) (Random.value * max);
    }
}
