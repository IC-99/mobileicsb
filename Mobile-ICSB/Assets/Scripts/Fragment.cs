using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{

    private RectTransform target;
    private SpriteRenderer sr;
    private float opacity;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("IconaFrammenti").GetComponent<RectTransform>();
        this.sr = this.GetComponent<SpriteRenderer>();
        this.opacity = 1f;
        Destroy(gameObject, 1f);
    }

    
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 4);
        this.sr.color = new Color(1f, 1f, 1f, opacity);
        opacity -= 0.03f;
    }

}
