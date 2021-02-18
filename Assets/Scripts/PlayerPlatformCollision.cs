using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformCollision : MonoBehaviour
{

    private GameObject currentPlatform;

    private Collider2D collider2d;



    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlatform != null && Input.GetAxis("Vertical") < 0 && !Input.GetKey(KeyCode.J)) {
            Physics2D.IgnoreCollision(currentPlatform.GetComponent<Collider2D>(), collider2d, true);            
        }
    }

    // Handling going down
    private void OnCollisionEnter2D(Collision2D other) {
        if (isPlatform(other.gameObject)) {
            currentPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (isPlatform(other.gameObject))
            currentPlatform = null;
    }

    private bool isPlatform(GameObject gameObject) {
        return gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).CompareTag("Platform Trigger");
    }



    // Handling going up
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Platform Trigger")) { 
            Debug.Log("hi");           
            Physics2D.IgnoreCollision(other.gameObject.transform.parent.gameObject.GetComponent<Collider2D>(), collider2d, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Platform Trigger")) {            
            Debug.Log("by");           
            Physics2D.IgnoreCollision(other.gameObject.transform.parent.gameObject.GetComponent<Collider2D>(), collider2d, false);
        }
    }
}
