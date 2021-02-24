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

    private Vector2 originalScale;
    
    private Animator animator;
    private GameObject currentFollow;
    public float visionRadius = 50f;

    // Start is called before the first frame update
    void Start() {
        originalScale = transform.localScale;

        // Ignore platforms
        var objects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < objects.Length; i++)
            if (objects[i].layer == PlatformCollision.PlatformLayer)
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), objects[i].GetComponent<Collider2D>(), true);
    
        currentFollow = GameObject.Find("Character"); // !!!
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() { 
        if (Vector2.Distance(currentFollow.transform.position, transform.position) <= visionRadius)
            FollowObject(currentFollow);
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

}
