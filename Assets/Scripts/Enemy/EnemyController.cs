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

    public bool isStaggered = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();        
        movementBehavior = GetComponent<MovementBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP.percentageOfMax() < staggerHP) {
            if (!isStaggered)
                StartCoroutine(BeginStagger());
            UpdateStaggered(true);
        }
        else UpdateStaggered(false);
    }

    // staggering podia ser um script tb se quisessemos por enemies sem stigger
    IEnumerator BeginStagger() {
        yield return new WaitForSeconds(staggerHP);
    }

    void UpdateStaggered(bool isStaggered) {
        this.isStaggered = isStaggered;
        movementBehavior.enabled = !isStaggered;
        attackBehavior.enabled = !isStaggered;
        animator.SetBool("Staggered", isStaggered); // sei la
    }

    // No staggered fazer o coiso do dash a cena é q tipo n há collisionins entre player e enemy
    // se calhar vamos ter de arranjar uma cena qq fixe, pq nos movements/attack behaviors tb dava jeito q
      // desse alguma maneira de detetar collisions entre enemy e players mas tipo nao para colidir mesmo tipo trust bro
}
