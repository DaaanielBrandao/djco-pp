using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaggerBehavior : MonoBehaviour {   
    public float staggerHP = 0.25f;
    public float staggerTime = 5f;
    public ParticleSystem dashKillEffect;
    
    private enum StaggerState {Ready, Staggered, Survivor};
    private StaggerState staggerState;

    private Animator animator;
    private EnemyHP enemyHP;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();
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
                GameObject player = GetPlayerCollision();
                if (player == null)
                    return;

                CharacterMovement movement = player.GetComponent<CharacterMovement>();
                if (staggerState == StaggerState.Staggered && movement.IsDashing()) {
                    movement.ResetDash();
                    enemyHP.Die();
                    Instantiate(dashKillEffect, transform.position, transform.rotation);
                }

                break;
            }
            case StaggerState.Survivor:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private GameObject GetPlayerCollision() {
        Collider2D[] collisions = new Collider2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(new string[]{"Player"}));
        if (GetComponent<Collider2D>().OverlapCollider(filter, collisions) > 0)
            return collisions[0].gameObject;     
        return null;
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
