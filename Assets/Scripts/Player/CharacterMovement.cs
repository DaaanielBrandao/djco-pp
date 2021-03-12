using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{   
    public float moveForce = 125;
    public float maxSpeed = 30;
    public float jumpForce = 35f;
    public float gravityMod = 7f;
    public Vector2 facingDir = new Vector2(1, 0);

    public Vector2 movement;
   
    private bool isOnGround = false;
    private bool isJumping = false;

    public enum DashState {Ready, Dashing, Cooldown, CooldownToReady, Waiting, WaveDash};
    public bool extraDash = false;
    public float dashTime = 0.2f;
    private float dashSpeed;
    public float defaultDashSpeed = 74;    
    public float dashCooldown = 0.2f;    
    public DashState dashState = DashState.Ready; 
    public Vector2 dashDir;

    public Color defaultDashColor;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D charCollider;

    public AudioClip dashSound;
    public AudioClip jumpSound;

    public GameObject mainCamera;
    public GameObject trail;
    public ParticleSystem dust;
    public ParticleSystem waveDust;
    public ParticleSystem dashDust; 
    
    private TrailRenderer trailRenderer;
    private PowerupList powerups;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        trailRenderer = trail.GetComponent<TrailRenderer>();
        charCollider = GetComponent<Collider2D>();
        powerups = GetComponent<PowerupList>();
        
        rb.gravityScale = gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDash();
        UpdateGroundTouch();
        HandleMove();
        HandleJump();
        HandleDash();
    }

    void UpdateGroundTouch() {
        Bounds colliderBounds = charCollider.bounds;
        Vector2 bottomCenter = colliderBounds.center - new Vector3(0, colliderBounds.extents.y);
        Vector2 boxSize = new Vector2(colliderBounds.extents.x * 2, 0.01f);

        RaycastHit2D[] hits = Physics2D.BoxCastAll(bottomCenter, boxSize, 0, Vector2.down, 0.01f, LayerMask.GetMask("Ground","Platform"));

        isOnGround = false;
        foreach (RaycastHit2D hit in hits) {
            if (!hit.collider.isTrigger && !Physics2D.GetIgnoreCollision(hit.collider, charCollider)) {
                isOnGround = true;
                break;
            }
        }

        if (isOnGround) {
            isJumping = false;
            extraDash = false;
        }
    }

    void HandleMove() {
        float inputHor = Input.GetAxisRaw("Horizontal");
        float inputVer = Input.GetAxisRaw("Vertical");

        movement = Vector3.right * (inputHor * Time.deltaTime * moveForce);
        
        // Movement (only add force if changing direction or if below max speed)
        if (inputHor * rb.velocity.x <= 0 || Mathf.Abs(rb.velocity.x) <= maxSpeed) {
            rb.AddForce(movement, ForceMode2D.Impulse);
        }


        // Drag (reduce velocity if over max speed - except for wavedash - or if user not trying to move)
        if(isOnGround && (dashState != DashState.WaveDash && Mathf.Abs(rb.velocity.x) > maxSpeed || inputHor == 0)){
            rb.velocity = new Vector2(rb.velocity.x * Mathf.Pow(0.00005f, Time.deltaTime) ,rb.velocity.y);
        }
        

        // Facing Direction
        Vector2 direction = new Vector2(inputHor, inputVer);        
        if (direction.x != 0) {
            facingDir = new Vector2(direction.x, direction.y);           
            transform.localScale = new Vector3(direction.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Out of bounds
        if (transform.position.y < -100)
            transform.position = new Vector3(0, 10, 0);

        animator.SetBool("Moving", inputHor != 0);
        animator.SetBool("Airborne",!isOnGround);
    }

    void HandleJump() {
        bool isGoingDown = rb.velocity.y < 0;
        
        if(Input.GetKeyDown(KeyCode.I) && isOnGround) { // && isOnGround && !gameOver){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; 


            SoundManager.Instance.Play(jumpSound);
            dust.Play();
        }

        if (Input.GetKeyUp(KeyCode.I) && isJumping && !isGoingDown) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }

        if(isGoingDown){ // im yelling timbeeeeeeeeeeeeeeeeeeer
            rb.gravityScale = gravityMod * 1.6f;
        }
        else{
            rb.gravityScale = gravityMod;
        }
    }
        
    void HandleDash() {
        trailRenderer.emitting = IsDashing();

        switch (dashState) {
            case DashState.Ready:
                dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                if (dashDir == Vector2.zero)
                    dashDir = facingDir.normalized;
                else dashDir = dashDir.normalized;

                if(Input.GetKeyDown(KeyCode.J)) {
                    dashState = DashState.Dashing;                 
                    StartCoroutine(DoDash());

                    SoundManager.Instance.Play(dashSound);
                    mainCamera.GetComponent<Animator>().SetTrigger("zoop");
                    dashDust.Play();
                }

                spriteRenderer.color = UnityEngine.Color.white;
                break;

            case DashState.Dashing:
                rb.velocity = dashDir * dashSpeed;

                if (isOnGround && dashDir.y < 0) { // only diagonal dash counts
                    dashState = DashState.WaveDash;            
                    waveDust.Play();
                }

                spriteRenderer.color = UnityEngine.Color.cyan;
                break;

            case DashState.Cooldown:
                if (isOnGround)
                    dashState = DashState.CooldownToReady;
                spriteRenderer.color = UnityEngine.Color.gray;
                break;
            case DashState.CooldownToReady:
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
                    rb.velocity = new Vector2(Mathf.Abs(dashSpeed * dashDir.x) * facingDir.x ,rb.velocity.y);
                    waveDust.Stop();
                }

                break;

            default: break;
        }
    }

    IEnumerator DoDash() {
        yield return new WaitForSeconds(dashTime);

        // Other factors can set DashState to ready, so leave if needed
        if (dashState == DashState.Ready)
            yield break; 

        if(dashState == DashState.WaveDash){
            dashState = DashState.Ready;
        }
        else if (extraDash){
            dashState = DashState.Ready;
            extraDash = false;
            rb.velocity = Vector2.zero;
        }
        else {
            dashState = DashState.Cooldown;
            rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(dashCooldown);
           
            if (dashState == DashState.CooldownToReady)
                dashState = DashState.Ready;
            else if (dashState != DashState.Ready)
                dashState = DashState.Waiting; 
        }
    }
    
    public void ResetDash() {
        if (IsDashing())
            extraDash = true;
        else dashState = DashState.Ready;
    }

    public bool IsDashing() {
        return dashState == DashState.Dashing || dashState == DashState.WaveDash;
    }
    
    public void Boioioing() {
        isOnGround = false;
        isJumping = false;
        dashState = DashState.Ready;
    }
    
    private void UpdateDash()
    {
        dashSpeed = powerups.HasPowerup("DashKill") ? powerups.GetPowerup("DashKill").original.dashSpeed : defaultDashSpeed;
        
        Color dashColor = powerups.HasPowerup("DashKill") ? powerups.GetPowerup("DashKill").original.particleColor : defaultDashColor;

        var dashPSMain = dashDust.main;
        var wavePSMain = waveDust.main;
        dashPSMain.startColor = dashColor;
        wavePSMain.startColor = dashColor;

        dashColor.a = 0.3f;
        trailRenderer.startColor = dashColor;
        trailRenderer.endColor = dashColor;
    }

}
