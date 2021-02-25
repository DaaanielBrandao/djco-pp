using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 60f;
    public float minJumpSpeed = 20f;
    public float maxJumpSpeed = 80f;
    // Start is called before the first frame update
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
            playerRb.velocity = new Vector2(playerRb.velocity.x,Mathf.Clamp(Mathf.Abs(playerRb.velocity.y),minJumpSpeed,maxJumpSpeed));
            //playerRb.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);
            cm.isOnGround = false;
            cm.isJumping = true;
            cm.trailRenderer.emitting = false;
            cm.dashState = CharacterMovement.DashState.Ready;
        }
    }
}
