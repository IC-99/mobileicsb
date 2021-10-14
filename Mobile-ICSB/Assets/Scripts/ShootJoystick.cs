using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootJoystick : MonoBehaviour
{
    public ShootJoystickIsUsing isUsing;

    public Transform player;
    private bool touchStart = false;

    private Vector2 pointA;

    public RectTransform shootKnob;
    public RectTransform shootOuter;

    public Rigidbody2D rb;


    void Start()
    {
        isUsing = FindObjectOfType<ShootJoystickIsUsing>();

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
            shootKnob.position = shootOuter.position;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset / 1500, 0.1f);
            moveCharacter(direction);
            shootKnob.position = new Vector2(shootOuter.position.x + direction.x, shootOuter.position.y + direction.y);
        }

    }
    void moveCharacter(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
