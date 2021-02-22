using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    public static int PlatformLayer = 9;
    public static string TriggerTag = "Platform Trigger";

    protected GameObject currentPlatform;
    protected Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        
    }
    // Handling going down
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == PlatformCollision.PlatformLayer)
            currentPlatform = other.gameObject;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.layer == PlatformCollision.PlatformLayer)
            currentPlatform = null;
    }

    // Handling going up
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(TriggerTag)) { 
            Physics2D.IgnoreCollision(other.gameObject.transform.parent.gameObject.GetComponent<Collider2D>(), collider2d, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag(TriggerTag)) {     
            Physics2D.IgnoreCollision(other.gameObject.transform.parent.gameObject.GetComponent<Collider2D>(), collider2d, false);
        }
    }
}
