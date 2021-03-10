using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStrafe : MovementBehavior
{
    public float safetyNet = 1.0f;
    public float speed = 10.0f;

    private Vector2 dir = Vector2.right;
    private Rigidbody2D rb;
    private Collider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
       transform.localScale = new Vector3(dir.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

       HandleEdges();
    }

    void HandleEdges() {
        Bounds bounds = enemyCollider.bounds;
        if (dir == Vector2.right) { // Going right
            TestEdge(bounds.min + new Vector3(bounds.size.x + safetyNet, 0, 0));    
        }
        else { // Going left
            TestEdge(bounds.min - new Vector3(safetyNet, 0, 0)); 
        }
    }

    private void TestEdge(Vector3 pos) {
        RaycastHit2D hitDown = GroundFinder.RayCast(pos, Vector2.down, 0.05f);
        RaycastHit2D hitWall = GroundFinder.RayCast(pos, dir, 0.05f);

        if (hitWall.collider || !hitDown.collider)
            dir = -dir;
    }
   
}
