using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    public float speed = 7.5f;
    public float minRange = 7.5f;
    public float maxRange = 10;

    public float circleSpeed = 1.5f;
    public int circleDir = 1;

    public float visionRadius = 50f;
   
    private Vector2 originalScale;    
    private PlayerDetector detector;
    private Animator animator;

    // Start is called before the first frame update
    void Start() {
        originalScale = transform.localScale;

        // Ignore platforms
        var objects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < objects.Length; i++)
            if (objects[i].layer == PlatformCollision.PlatformLayer)
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), objects[i].GetComponent<Collider2D>(), true);
    
        animator = GetComponent<Animator>();
        detector = gameObject.AddComponent<PlayerDetector>();
        detector.radius = visionRadius;
    }

    // Update is called once per frame
    void Update() { 
        GameObject follow = detector.nearestPlayer;
        if (follow != null && Vector2.Distance(follow.transform.position, transform.position) <= visionRadius)
            FollowObject(follow);
      /*
        else {
             float x = (Mathf.PerlinNoise(transform.position.x, transform.position.y) - 0.5f);
             float y = (Mathf.PerlinNoise(transform.position.y, transform.position.x) - 0.5f);
             Debug.Log("PERLIN: " + x + "  " + y);
             transform.Translate(new Vector3(x,0,0) * Time.deltaTime);
        }
        */ //TEM POTENCIAL SE CALHAR
    }

    void FollowObject(GameObject follow) {
        Vector2 dir = follow.transform.position - gameObject.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float distance = dir.magnitude;

        if (distance == 0 && minRange > 0)
            dir = new Vector2(0, 1);

        // Move
        if (distance > maxRange)
            dir = dir.normalized;
        else if (distance < minRange)
            dir = -dir.normalized;
        else {
            dir = Vector2.zero;
        }
        dir.y *= 2;

        //Debug.Log(dir);
        
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
        transform.RotateAround(follow.transform.position, Vector3.forward, circleDir * circleSpeed * 360 / distance * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            circleDir = -circleDir;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,visionRadius);
    }

}
