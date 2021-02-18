using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{   
    public float speed = 14f;
    public float jumpForce = 25f;
    public float gravityMod = 5.5f;
    public Vector2 direction = new Vector2(1, 0);
    public int turned = 1;

    public Vector3 movement;
   
    private bool isOnGround = false;
    private bool isJumping = false;
    private bool hasDash = false;

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
        float inputVer = Input.GetAxisRaw("Vertical");

        direction = new Vector2(
            inputHor == 0 ? 0 : Mathf.Sign(inputHor), 
            inputVer == 0 ? 0 : Mathf.Sign(inputVer)
        ).normalized;
        movement = Vector3.right * inputHor * Time.deltaTime * speed;
        transform.Translate(movement);

        if (direction.x < 0){
            transform.localScale = new Vector3(-1,1,1);
            turned = -1;
        }
        else if (direction.x > 0){
            transform.localScale = new Vector3(1,1,1);
             turned = +1;
        }
    }

    public void HandleJump() {
        bool isGoingDown = rb.velocity.y < 0;
        
        if(Input.GetKeyDown(KeyCode.I) && isOnGround) { // && isOnGround && !gameOver){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; 


            SoundManager.Instance.OnJump();
        }

        if (Input.GetKeyUp(KeyCode.I) && isJumping && !isGoingDown) {
           // rb.velocity += new Vector2(0, -10f);
            rb.velocity /= 2;
        }

        if(isGoingDown){ // im yelling timbeeeeeeeeeeeeeeeeeeer
            Physics2D.gravity = defGrav * 1.6f;
        }
        else{
            Physics2D.gravity = defGrav;
        }
    }
        

    public void HandleDash(){
        
        if(Input.GetKeyDown(KeyCode.J) && hasDash) {
            Debug.Log("dash!");
            hasDash = false;
            spriteRenderer.color = UnityEngine.Color.blue;
            
            dashDir = direction;            
            currentDashTime = Time.deltaTime;

            SoundManager.Instance.OnDash();
        } else if (currentDashTime > 0) {
            currentDashTime += Time.deltaTime;
            if(currentDashTime >= dashTime) {
                Debug.Log("StopDash");
                currentDashTime = 0;
                spriteRenderer.color = UnityEngine.Color.green;
                rb.velocity = dashDir * Time.deltaTime * speed;
                if(isOnGround){
                    spriteRenderer.color = UnityEngine.Color.red;
                    hasDash = true;
                }
            }
            else rb.velocity = dashDir * dashSpeed;
        } 
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
            isJumping = false;
            
            SoundManager.Instance.OnDrop();
            
            hasDash = true;
            spriteRenderer.color = UnityEngine.Color.red;

            if (currentDashTime > 0) {
                Debug.Log("StopDash");
                currentDashTime = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = false;
        }
    }
}
