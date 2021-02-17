using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravityMod = 2.5f;
    public bool isOnGround = false;
    private Vector2 defGrav;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defGrav = Physics2D.gravity * gravityMod;
        Physics2D.gravity = defGrav;
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * input * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) {// && isOnGround && !gameOver){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //isOnGround = false;
        }

        if(rb.velocity.y < 0){
            Physics2D.gravity = defGrav * 2f;
        }
        else{
            Physics2D.gravity = defGrav;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            Debug.Log("ola");
            isOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            Debug.Log("xau");
            isOnGround = false;
        }
    }
}
