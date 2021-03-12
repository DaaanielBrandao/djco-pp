using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private MovementBehavior movementBehavior;
    private AttackBehavior attackBehavior;
    private StaggerBehavior staggerBehavior;
    private EnemyHP enemyHp;
    private Collider2D enemyCollider;
    
    public ParticleSystem dashKillEffect;

    // Start is called before the first frame update
    void Start()
    {
        movementBehavior = GetComponent<MovementBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
        staggerBehavior = GetComponent<StaggerBehavior>();
        enemyHp = GetComponent<EnemyHP>();
        enemyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementBehavior.enabled = !staggerBehavior.IsStaggered();
        attackBehavior.enabled = !staggerBehavior.IsStaggered();

        // DashKill
        GameObject player = PlayerDetector.GetPlayerCollision(enemyCollider);
        if (player) {
            CharacterMovement movement = player.GetComponent<CharacterMovement>();
            PowerupList powerups = player.GetComponent<PowerupList>();
            if (movement.IsDashing() && powerups.HasPowerup("DashKill"))
            {
                dashKill(player);
            }

        }
    }
    //secalhar mudar de sitio
    public void dashKill(GameObject player)
    {
        CharacterMovement movement = player.GetComponent<CharacterMovement>();
        PlayerHP playerHp = player.GetComponent<PlayerHP>();
        
        
        enemyHp.Die();
        movement.ResetDash();
        playerHp.healDashKill();
        Instantiate(dashKillEffect, transform.position, transform.rotation);
    }
}
