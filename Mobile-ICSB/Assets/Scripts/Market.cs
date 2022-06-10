using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{

    public GameObject marketPanel;
    public Transform playerPosition;
    public Image coloreBottoneCompraElmo;
    public Text TestoBottoneCompraElmo;
    public Image coloreBottoneCompraFreq;
    public Text TestoBottoneCompraFreq;
    public PlayerHealth player;
    public ShootJoystick shooting;
    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        marketPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(playerPosition.position, transform.position) < 20){
            marketPanel.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    public void exit()
    {
        marketPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void compraElmo()
    {
        if (score.getFrammenti() >= 6)
        {
            score.reduceFragment(6);
            player.setHelmet();
            coloreBottoneCompraElmo.color = Color.green;
            TestoBottoneCompraElmo.text = "LO POSSIEDI GIÀ";
        }
    }

    public void compraFreq()
    {
        if (score.getFrammenti() >= 5)
        {
            score.reduceFragment(5);
            shooting.setIsFreq();
            coloreBottoneCompraFreq.color = Color.green;
            TestoBottoneCompraFreq.text = "LO POSSIEDI GIÀ";
        }
    }


}
