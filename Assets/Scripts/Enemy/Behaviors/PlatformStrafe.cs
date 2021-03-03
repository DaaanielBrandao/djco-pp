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
        string[] layers = {"Ground", "Platform"};
       
        Physics2D.queriesHitTriggers = false;

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 0.05f, LayerMask.GetMask(layers));

        Physics2D.queriesHitTriggers = true;

        if (hit.collider == null)
            dir = -dir;
    }
   
}
