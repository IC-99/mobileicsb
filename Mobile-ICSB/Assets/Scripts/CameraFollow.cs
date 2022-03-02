using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 9.0f;
    public Vector3 offset;
    private float x;
    private float y;


    void Start()
    {
        transform.position = new Vector3(0, -104, -1);
    }


    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = calcolo(desiredPosition, smoothedPosition);
    }

    private Vector3 calcolo(Vector3 desiredPosition, Vector3 smoothedPosition)
    {
        if (smoothedPosition.x < 0f) this.x = 0f;
        else
        {
            if (smoothedPosition.x > 111f) this.x = 111f;
            else this.x = smoothedPosition.x;
        }

        if (smoothedPosition.y < -104f) this.y = -104f;
        else
        {
            if (smoothedPosition.y > 46f) this.y = 46f;
            else this.y = smoothedPosition.y;
        }

        return new Vector3(this.x, this.y, -1f);
    }
}
