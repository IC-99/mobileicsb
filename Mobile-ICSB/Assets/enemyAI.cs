using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public float aggroDistance;
    public float minDistance;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


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

        if (path.GetTotalLength() > aggroDistance)
            return;

        if(path.GetTotalLength() < minDistance)
        {
            rb.drag = 20;
            return;
        }


        if (currentWaypoint >= path.vectorPath.Count)   
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

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
