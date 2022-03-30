using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollaFiume : MonoBehaviour
{

    private SpriteRenderer sr;
    private float opacity;
    private bool isGrowing;

    // Start is called before the first frame update
    void Start()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
        this.opacity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.opacity < 1)
        {
            this.opacity += 0.003f * estrazioneCasuale(4);
        }
        else
        {
            this.opacity = 0f;
        }
        this.sr.color = new Color(1f, 1f, 1f, this.opacity);
        transform.localScale = new Vector3(7 * opacity, 7 * opacity, 1 * opacity);
    }

    private int estrazioneCasuale(int max)
    {
        return (int)(Random.value * max); //attendi almeno un secondo
    }

}
