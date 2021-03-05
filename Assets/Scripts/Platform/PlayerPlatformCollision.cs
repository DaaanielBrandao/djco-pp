using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformCollision : PlatformCollision
{
    // Update is called once per frame
    void Update()
    {
        // Dash down
        if (currentPlatform != null && Input.GetKeyDown(KeyCode.S) && !Input.GetKey(KeyCode.J)) { // AAAAAAAAAA!
            Physics2D.IgnoreCollision(currentPlatform.GetComponent<Collider2D>(), collider2d, true);            
        }
    }
}
