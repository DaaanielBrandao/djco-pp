using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float staggerHP = 0.25f;
    public float staggerTime = 5f;

    private Animator animator;

    private EnemyHP enemyHP;
    private MovementBehavior movementBehavior;
    private AttackBehavior attackBehavior;

    public enum StaggerState {Ready, Staggered, Survivor};
    public StaggerState staggerState;


    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();        
        movementBehavior = GetComponent<MovementBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
        staggerState = StaggerState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (staggerState == StaggerState.Ready && enemyHP.percentageOfMax() < staggerHP) {
            StartCoroutine(Stagger());
        }
    }

    // staggering podia ser um script tb se quisessemos por enemies sem stigger
    IEnumerator Stagger() {
        UpdateStaggered(true);
        yield return new WaitForSeconds(staggerTime);
        UpdateStaggered(false);
    }

    void UpdateStaggered(bool isStaggered) {
        if (isStaggered)
            staggerState = StaggerState.Staggered;
        else staggerState = StaggerState.Survivor;

        movementBehavior.enabled = !isStaggered;
        attackBehavior.enabled = !isStaggered;
        animator.SetBool("staggered", isStaggered); 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            CharacterMovement movement = other.GetComponentInParent<CharacterMovement>();
            if (staggerState == StaggerState.Staggered && movement.IsDashing()) {
                movement.ResetDash();
                enemyHP.Kill();
            }
        }
    }

    // No staggered fazer o coiso do dash a cena é q tipo n há collisionins entre player e enemy
    // se calhar vamos ter de arranjar uma cena qq fixe, pq nos movements/attack behaviors tb dava jeito q
      // desse alguma maneira de detetar collisions entre enemy e players mas tipo nao para colidir mesmo tipo trust bro
}
