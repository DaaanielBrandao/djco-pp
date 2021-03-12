using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerBehavior : MonoBehaviour {   
    public float staggerHP = 0.25f;
    public float staggerTime = 5f;

    private enum StaggerState {Ready, Staggered, Survivor};
    private StaggerState staggerState;

    private Animator animator;
    private EnemyHP enemyHP;
    private Collider2D enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        staggerState = StaggerState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        switch (staggerState)
        {
            case StaggerState.Ready:
                if (enemyHP.PercentageOfMax() < staggerHP)
                    StartCoroutine(Stagger());
                break;
            case StaggerState.Staggered:
            {
                GameObject player = PlayerDetector.GetPlayerCollision(enemyCollider);
                if (player == null)
                    return;

                CharacterMovement movement = player.GetComponent<CharacterMovement>();
                if (staggerState == StaggerState.Staggered && movement.IsDashing())
                {
                    gameObject.GetComponent<EnemyController>().dashKill(player);
                }

                break;
            }
            case StaggerState.Survivor:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    IEnumerator Stagger() {

        staggerState = StaggerState.Staggered;
        animator.SetBool("staggered", true); 
        yield return new WaitForSeconds(staggerTime);
        animator.SetBool("staggered", false); 
        staggerState = StaggerState.Survivor;
    }

    public bool IsStaggered() {
        return staggerState == StaggerState.Staggered;
    }
}
