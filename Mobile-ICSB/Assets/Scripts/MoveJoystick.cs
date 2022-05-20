using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour
{
    public MoveJoystickIsUsing isUsing;

    public Transform player;
    public float speed = 200.0f;
    private bool touchStart = false;

    private Vector3 pointA;

    public RectTransform moveKnob;
    public RectTransform moveOuter;

    public Rigidbody2D rb;

    private bool inuso = false;
    private Touch touch;



    void Start()
    {
        isUsing = FindObjectOfType<MoveJoystickIsUsing>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (!inuso)
            {
                pointA = touch.position;
                inuso = true;
            }
        }

        if (Input.touchCount == 2)
        {

            touch = touchSinistro(Input.GetTouch(0), Input.GetTouch(1));
            if (!inuso)
            {
                pointA = touch.position;
                inuso = true;
            }
        }

        if (isUsing.Pressed)
        {
            touchStart = true;
        }
        else
        {
            inuso = false;
            touchStart = false;
            moveKnob.position = moveOuter.position;
        }
    }
    

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = new Vector3(touch.position.x, touch.position.y, 0) - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset / 1500, 0.1f);
            moveCharacter(direction);
            moveKnob.position = new Vector2(moveOuter.position.x + direction.x * 10, moveOuter.position.y + direction.y * 10) ;
        }

    }
    void moveCharacter(Vector2 direction)
    {
        rb.position = new Vector2(rb.position.x + direction.x * speed * Time.deltaTime, rb.position.y + direction.y * speed * Time.deltaTime);
    }

    public Touch touchSinistro(Touch t1, Touch t2)
    {
        if(t1.position.x < t2.position.x)
        {
            return t1;
        }
        return t2;

    }

    public bool isMoving()
    {
        return this.inuso;
    }

}