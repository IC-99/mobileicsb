using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentCountText : MonoBehaviour
{

    public Score score;
    private Text fragmentCounter;


    void Start()
    {
        fragmentCounter = GetComponent<Text>();
    }

    void Update()
    {
        fragmentCounter.text = score.getFrammenti().ToString();
    }
}
