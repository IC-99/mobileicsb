using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour
{
    public MoveJoystickIsUsing isUsing;

    public Transform player;
    public float speed = 20.0f;
    private bool touchStart = false;

    private Vector2 pointA;

    public RectTransform moveKnob;
    public RectTransform moveOuter;

    public Rigidbody2D rb;


    void Start()
    {
        isUsing = FindObjectOfType<MoveJoystickIsUsing>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Input.mousePosition;
        }
        if (isUsing.Pressed)
        {
            touchStart = true;
        }
        else
        {
            touchStart = false;
            moveKnob.position = moveOuter.position;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset / 1500, 0.1f);
            moveCharacter(direction);
            moveKnob.position = new Vector2(moveOuter.position.x + direction.x, moveOuter.position.y + direction.y) ;
        }

    }
    void moveCharacter(Vector2 direction)
    {
        player.Translate(new Vector2(0f, direction.magnitude) * speed * Time.deltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}