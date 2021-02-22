using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{   
    public float speed = 25;
    public float jumpForce = 35f;
    public float gravityMod = 7f;
    public Vector2 facingDir = new Vector2(1, 0);

    public Vector2 movement;
   
    private bool isOnGround = false;
    private bool isJumping = false;

    public enum DashState {Ready, Dashing, Cooldown, Waiting, WaveDash};
    public float dashTime = 0.2f;
    public float dashSpeed = 40f;    
    public float dashCooldown = 0.2f;    
    public DashState dashState = DashState.Ready; 
    public Vector2 dashDir;
    
    private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject mainCamera;
    public GameObject trail;
    private TrailRenderer trailRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        trailRenderer = trail.GetComponent<TrailRenderer>();
        rb.gravityScale = gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleJump();
        HandleDash();
    }

    public void HandleMove() {
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");

        movement = Vector3.right * inputHor * Time.deltaTime * speed;
        //transform.Translate(movement);
        if(!(inputHor * rb.velocity.x > 0 && Mathf.Abs(rb.velocity.x) > 30)){
            rb.AddForce(movement * 5, ForceMode2D.Impulse);
        }

    /*
        if(isOnGround && ((inputHor == 0 && Mathf.Abs(rb.velocity.x) > 0) ||  Mathf.Abs(rb.velocity.x) > 30)){
            Debug.Log("ola");
            rb.AddForce(Vector3.right* -5 * Mathf.Sign(rb.velocity.x), ForceMode2D.Impulse);
        }*/
        if(!isJumping && (dashState != DashState.WaveDash && Mathf.Abs(rb.velocity.x) > 30 || inputHor == 0)){
            rb.velocity = new Vector2(rb.velocity.x * Mathf.Pow(0.00005f,Time.deltaTime) ,rb.velocity.y);
        }
        

        Vector2 direction = new Vector2(inputHor, inputVer);        
        if (direction.x != 0) {
            facingDir = new Vector2(direction.x, direction.y);           
            transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (transform.position.y < -50)
            transform.position = new Vector3(0, 10, 0);

        

        //animator.SetInteger("HorizontalMov", (int)Mathf.Ceil(inputHor));
        //animator.SetBool("Airborne", isJumping);
        bool moving = false;
        if(inputHor != 0)
            moving = true;
        animator.SetBool("Moving",moving);
        animator.SetBool("Airborne",!isOnGround);
    }

    public void HandleJump() {
        bool isGoingDown = rb.velocity.y < 0;
        
        if(Input.GetKeyDown(KeyCode.I) && isOnGround) { // && isOnGround && !gameOver){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; 


            SoundManager.Instance.OnJump();
            // animator.SetTrigger("Jump");
        }

        if (Input.GetKeyUp(KeyCode.I) && isJumping && !isGoingDown) {
           // rb.velocity += new Vector2(0, -10f);
            rb.velocity /= 2;
        }

        if(isGoingDown){ // im yelling timbeeeeeeeeeeeeeeeeeeer
            rb.gravityScale = gravityMod * 1.6f;
        }
        else{
            rb.gravityScale = gravityMod;
        }
    }
        
    public void HandleDash() {
        switch (dashState) {
            case DashState.Ready:
                dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if (dashDir == Vector2.zero)
                    dashDir = facingDir.normalized;
                else dashDir = dashDir.normalized;

                if(Input.GetKeyDown(KeyCode.J)) {
                    dashState = DashState.Dashing;                 
                    StartCoroutine(DoDash());

                    SoundManager.Instance.OnDash();
                    mainCamera.GetComponent<Animator>().SetTrigger("zoop");
                    trailRenderer.emitting = true;
                }

                spriteRenderer.color = UnityEngine.Color.white;
                break;

            case DashState.Dashing:
                rb.velocity = dashDir * dashSpeed;
                spriteRenderer.color = UnityEngine.Color.cyan;
                break;

            case DashState.Cooldown:
                spriteRenderer.color = UnityEngine.Color.gray;
                break;

            case DashState.Waiting:
                if (isOnGround)
                    dashState = DashState.Ready;
                spriteRenderer.color = UnityEngine.Color.gray;
                break;
            case DashState.WaveDash:
                rb.velocity = new Vector2(dashSpeed * dashDir.x ,rb.velocity.y);
                if(Input.GetKeyDown(KeyCode.I)){
                    dashState = DashState.Ready;
                    trailRenderer.emitting = false;
                    //HandleJump();
                }
                break;

            default: break;
        }
    }

    IEnumerator DoDash() {
        yield return new WaitForSeconds(dashTime);

        if (dashState != DashState.Ready) {
            trailRenderer.emitting = false;
            if(dashState == DashState.WaveDash)
                dashState = DashState.Ready;
            else {
                dashState = DashState.Cooldown;
                rb.velocity = Vector2.zero;
            }

            yield return new WaitForSeconds(dashCooldown);

            if (dashState != DashState.Ready && dashState != DashState.WaveDash)
                dashState = DashState.Waiting;                   
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
            isJumping = false;
            
            SoundManager.Instance.OnDrop();

            if(dashState == DashState.Dashing){
               dashState = DashState.WaveDash;
               //rb.velocity = new Vector2(dashSpeed * dashDir.x ,rb.velocity.y);
               //trailRenderer.emitting = false;
            }
            else trailRenderer.emitting = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = false;
        }
    }
}
