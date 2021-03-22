using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class FollowAI : MovementBehavior
{
    public float speed = 7.5f;
    public float minRange = 7.5f;
    public float maxRange = 10;

    public float circleSpeed = 1.5f;
    public int circleDir = 1;

    public float visionRadius = 50f;

    public bool goingAway = false; // :(
   
    private Vector2 originalScale;    
    private PlayerDetector detector;
    private Animator animator;

    private Path path;
    private int currentWayPoint = 0;
    public float nextWayPointDistance = 3f;
    private bool reachedEndPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        originalScale = transform.localScale;

        // Ignore platforms
        var objects = FindObjectsOfType<GameObject>();
        foreach (var t in objects)
            if (t.layer == PlatformCollision.PlatformLayer)
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), t.GetComponent<Collider2D>(), true);

        animator = GetComponent<Animator>();
        detector = gameObject.AddComponent<PlayerDetector>();
        detector.radius = visionRadius;
        InvokeRepeating(nameof(UpdatePath),0f,.05f);
    }

    // Update is called once per frame
    void FixedUpdate() { 
        GameObject follow = detector.nearestPlayer;
        if (follow != null && Vector2.Distance(follow.transform.position, transform.position) <= visionRadius)
            FollowObject(follow);
        else Wander();

        AvoidOtherEnemies();
    }

    void UpdatePath()
    {
        if(detector.nearestPlayer != null && seeker.IsDone())
        {
           // Debug.Log(detector.nearestPlayer.transform.position);
            seeker.StartPath(rb.position, detector.nearestPlayer.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            //Debug.Log("newpath!");
            path = p;
            currentWayPoint = 0;
        }
    }
    
    void AvoidOtherEnemies() {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        float colliderRadius = collider.bounds.extents.x;

        LayerMask mask = LayerMask.GetMask(new string[]{"Enemy"});
        Collider2D[] hits = Physics2D.OverlapCircleAll(collider.bounds.center, colliderRadius + 2, mask);

        foreach (Collider2D hit in hits) {
            if (hit.gameObject != gameObject) {
                if (transform.position.x > hit.gameObject.transform.position.x) {
                    StartCoroutine(GoAway());
                }
                break;
            }
        }
    }

    IEnumerator GoAway() {
        goingAway = true;
        yield return new WaitForSeconds(1.0f);
        goingAway = false;
    }

    void Wander() {
       // float x = (Mathf.PerlinNoise(transform.position.x, transform.position.y) - 0.5f);
       // float y = (Mathf.PerlinNoise(transform.position.y, transform.position.x) - 0.5f);
       // Debug.Log("PERLIN: " + x + "  " + y);
       // transform.Translate(new Vector3(x, y));
    }

    void FollowObject(GameObject follow)
    {
        
        Vector2 dir;
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;

        }
        
        reachedEndPath = false;
        dir = ((Vector2) path.vectorPath[Math.Min(currentWayPoint + 5, path.vectorPath.Count - 1)] - rb.position).normalized;
        float distanceToWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        //Debug.Log("wapoint: " + currentWayPoint + " " + dir + "distancetoway: " + distanceToWaypoint);
        
        if (distanceToWaypoint < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float distance =  (follow.transform.position - gameObject.transform.position).magnitude;
       // Debug.Log("Distance: " + distance);
        //float distance = dir.magnitude;
        
        if (distance == 0 && minRange > 0) // Edge case
            dir = new Vector2(0, 1);

        // Move
        if (distance > maxRange) { // Towards player
           // Debug.Log("chasing");
            dir.x *= 1.5f;

        }
        else if (distance < minRange)
        {
            // Away from player
            dir = -dir/ 2;
            //Debug.Log("awaying");
        }

        //Debug.Log(dir.magnitude);
        //Vector2 force = dir * speed * Time.deltaTime;
        //rb.AddForce(force);
        
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        
        // Rotate
        if (angle > 90 || angle < -90) {
            transform.localScale = new Vector2(-originalScale.x, originalScale.y);
            transform.rotation = Quaternion.Euler(0, 0, 180 + angle);
        }
        else {
            transform.localScale = originalScale;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Circle
        //transform.RotateAround(follow.transform.position, Vector3.forward, circleDir * circleSpeed * 360 / distance * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            circleDir = -circleDir;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere (path.vectorPath[Math.Min(currentWayPoint + 5, path.vectorPath.Count - 1)],1);
    }
}
