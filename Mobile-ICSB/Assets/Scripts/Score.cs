using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public double score;
    public int frammenti;

    // Start is called before the first frame update
    void Start()
    {
        frammenti = 0;
        InvokeRepeating("updateScore", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void updateScore()
    {
        this.score -= 1;
    }

    public void addScore(double toAdd)
    {
        this.score += toAdd;
    }

    public double getScore()
    {
       return this.score;
    }

    public void addFragment(int amount)
    {
        this.frammenti += amount;
    }

    public int getFrammenti()
    {
        return this.frammenti;
    }
}
