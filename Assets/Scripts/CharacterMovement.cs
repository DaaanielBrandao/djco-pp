using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 14f;
    public float jumpForce = 25f;
    public float gravityMod = 5.5f;
    
    private bool isOnGround = false;
    private bool isJumping = false;
    private Vector2 direction = new Vector2(1, 0);

    private Vector2 defGrav;
    private Rigidbody2D rb;
    

    public float currentDashTime = 0;
    public float dashTime = 0.15f;
    public float dashSpeed = 30f;
    public float dashCooldown = 2f;
    public Vector2 dashDir;

    
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defGrav = Physics2D.gravity * gravityMod;
        Physics2D.gravity = defGrav;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleJump();
        HandleDash();
    }

    public void HandleMove() {
        float inputHor = Input.GetAxis("Horizontal");
        float inputVer = Input.GetAxis("Vertical");

        Debug.Log(new Vector2(Mathf.Sign(inputHor), Mathf.Sign(inputVer)));

        direction = new Vector2(
            inputHor == 0 ? 0 : Mathf.Sign(inputHor), 
            inputVer == 0 ? 0 : Mathf.Sign(inputVer)
        ).normalized;

        transform.Translate(Vector3.right * inputHor * Time.deltaTime * speed);
    }

    public void HandleJump() {
        bool isGoingDown = rb.velocity.y < 0;
        
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) { // && isOnGround && !gameOver){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; 
        }

        if (Input.GetKeyUp(KeyCode.Space) && isJumping && !isGoingDown) {
           // rb.velocity += new Vector2(0, -10f);
            rb.velocity /= 2;
        }

        if(isGoingDown){ // im yelling timbeeeeeeeeeeeeeeeeeeer
            Physics2D.gravity = defGrav * 2f;
        }
        else{
            Physics2D.gravity = defGrav;
        }
    }
        

    public void HandleDash(){
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && currentDashTime == 0) {
            Debug.Log("dash!");
            spriteRenderer.color = UnityEngine.Color.blue;
            if (direction == Vector2.zero)
                dashDir = new Vector2(1, 0);
            else dashDir = direction;
            
            currentDashTime += Time.deltaTime;
        } else if (currentDashTime > 0) {
            currentDashTime += Time.deltaTime;
            if(currentDashTime >= dashTime) {
                Debug.Log("StopDash");
                spriteRenderer.color = UnityEngine.Color.yellow;

                currentDashTime = -dashCooldown;
               // direction = Input.GetAxis("Horizontal");
                rb.velocity = dashDir * Time.deltaTime * speed;
            }
            else rb.velocity = dashDir * dashSpeed;
        } else if (currentDashTime < 0) {
            currentDashTime += Time.deltaTime;
            if (currentDashTime >= 0) {
                spriteRenderer.color = UnityEngine.Color.red;
                
                currentDashTime = 0;
                Debug.Log("Dash Ready!");
            }
        } 
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = false;
        }
    }
}
