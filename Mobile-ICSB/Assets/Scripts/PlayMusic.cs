using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource music;

    void Start()
    {
        this.music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
