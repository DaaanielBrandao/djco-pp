using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    public float knockbackForce = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("boing!");
        GameObject go = other.gameObject;
        
        if(other.gameObject.name == "Character"){
            CharacterMovement cm = other.gameObject.GetComponent<CharacterMovement>();
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 knockbackDir = (go.transform.position - gameObject.transform.position).normalized;
            //playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(Mathf.Abs(playerRb.velocity.y), minJumpSpeed, maxJumpSpeed));
            playerRb.velocity = knockbackDir * knockbackForce;
            //playerRb.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);
            cm.Boioioing();
        }
    }
}
