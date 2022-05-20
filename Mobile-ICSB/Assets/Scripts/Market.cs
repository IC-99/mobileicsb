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

    // Start is called before the first frame update
    void Start()
    {
        marketPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getHasHelmet())
        {
            coloreBottoneCompraElmo.color = Color.green;
            TestoBottoneCompraElmo.text = "LO POSSIEDI GIÀ";
        }
        if (shooting.getIsFreq())
        {
            coloreBottoneCompraFreq.color = Color.green;
            TestoBottoneCompraFreq.text = "LO POSSIEDI GIÀ";
        }
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
        //TODO togli soldi
        player.setHelmet();
    }

    public void compraFreq()
    {
        //TODO togli soldi
        shooting.setIsFreq();
    }


}
