using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public Transform GfxTransform;
    public Rigidbody2D GfxRB;
    public Rigidbody2D CanvasRB;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public float aggroDistance;
    public float minDistance;
    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public bool canRotate;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if (this.canRotate)
        {
            Vector2 direzioneRotazione = new Vector2(target.position.x, target.position.y) - (Vector2)this.GfxRB.position;
            direzioneRotazione = direzioneRotazione.normalized;
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direzioneRotazione);
            GfxTransform.rotation = Quaternion.RotateTowards(GfxTransform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (path.GetTotalLength() > aggroDistance)
            return;

        if(path.GetTotalLength() < minDistance)
        {
            rb.drag = 20;
            return;
        }


/*        if (currentWaypoint >= path.vectorPath.Count)   
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }*/

        rb.drag = 1;


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);



        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
