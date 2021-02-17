using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 14f;
    public float jumpForce = 25f;
    public float gravityMod = 5.5f;
    public bool isOnGround = false;
    public float currentDashTime = 0;
    public float dashTime = 0.15f;

    public float dashSpeed = 30f;

    public float dashCooldown = 2f;
    public int dashDir;
    private Vector2 defGrav;
    private float direction;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defGrav = Physics2D.gravity * gravityMod;
        Physics2D.gravity = defGrav;
    }
    private void checkJump(){
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround){
            Debug.Log("Jump!");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void checkDash(){
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && currentDashTime == 0){
            Debug.Log("dash!");
            spriteRenderer.color = UnityEngine.Color.blue;
            dashDir = 1;
            if(direction < 0)
                dashDir = -1;
            currentDashTime += Time.deltaTime;
        } else if(currentDashTime > 0){
            rb.velocity = Vector2.right * dashDir * dashSpeed;
            currentDashTime += Time.deltaTime;
            if(currentDashTime >= dashTime){
                Debug.Log("StopDash");
                spriteRenderer.color = UnityEngine.Color.yellow;

                currentDashTime = -dashCooldown;
                direction = Input.GetAxis("Horizontal");
                rb.velocity = Vector2.right * dashDir * Time.deltaTime * speed;
            }
        } else if(currentDashTime < 0){
             currentDashTime += Time.deltaTime;
             if(currentDashTime >= 0){
                spriteRenderer.color = UnityEngine.Color.red;
                
                currentDashTime = 0;
                Debug.Log("Dash Ready!");
             }
        } 
    }
    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * direction * Time.deltaTime * speed);
        checkJump();
        checkDash();

        if(rb.velocity.y < 0){
            Physics2D.gravity = defGrav * 2f;
        }
        else{
            Physics2D.gravity = defGrav;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = false;
        }
    }
}
