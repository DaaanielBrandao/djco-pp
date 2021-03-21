using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Follow : MovementBehavior
{
    public float speed = 7.5f;
    public float circleSpeed = 2f;
    public float minRange = 7.5f;
    public float maxRange = 10;

    public bool ignoreGround = false;
    public bool ignorePlatforms = false;
    
    public float visionRadius = 50f;
    private PlayerDetector detector;
    private Vector2 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        // Ignore platforms
        GameObject[] objects = FindObjectsOfType<GameObject>();
        Collider2D thisCollider = gameObject.GetComponent<Collider2D>();
        foreach (var obj in objects)
        {
            if (ignoreGround && obj.layer == LayerMask.NameToLayer("Ground")) {
                Physics2D.IgnoreCollision(thisCollider, obj.GetComponent<CompositeCollider2D>(), true);
                Physics2D.IgnoreCollision(thisCollider, obj.GetComponent<TilemapCollider2D>(), true);
            }
            
            if (ignorePlatforms && obj.layer == PlatformCollision.PlatformLayer)
                Physics2D.IgnoreCollision(thisCollider, obj.GetComponent<Collider2D>(), true);
        }


        detector = gameObject.AddComponent<PlayerDetector>();
        detector.radius = visionRadius;
        
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject follow = detector.nearestPlayer;
        if (follow && Vector2.Distance(follow.transform.position, transform.position) <= visionRadius)
            FollowObject(follow);
  
    }

    private void FollowObject(GameObject follow)
    {
        Vector2 dir = follow.transform.position - gameObject.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        float distance = dir.magnitude;
        
        if (distance == 0) // Edge case
            dir = new Vector2(0, 1);
        
        // Move
        if (distance > maxRange) { // Towards player
           // Debug.Log(distance + "b");
            dir = dir.normalized;
            dir.x *= 1.5f;
        }
        else if (distance < minRange) { // Away from player
            dir = -dir.normalized / 2; 
           // Debug.Log(distance + "m");
        }
        else {
            dir = Vector2.zero;
        }
        
        transform.Translate(dir * (speed * Time.deltaTime), Space.World);
        
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
        if (dir == Vector2.zero)
            transform.RotateAround(follow.transform.position, Vector3.forward, circleSpeed * 360 / distance * Time.deltaTime);
        
    }
}
