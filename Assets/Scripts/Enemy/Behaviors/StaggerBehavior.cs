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
        if (staggerState == StaggerState.Ready && enemyHP.percentageOfMax() < staggerHP) {
            StartCoroutine(Stagger());
        }
        else if (staggerState == StaggerState.Staggered) {
            GameObject player = GetPlayerCollision();
            if (player == null)
                return;

            CharacterMovement movement = player.GetComponent<CharacterMovement>();
            if (movement == null)
                Debug.Log(player);
            if (staggerState == StaggerState.Staggered && movement.IsDashing()) {
                movement.ResetDash();
                enemyHP.Die();
                Instantiate(dashKillEffect, transform.position, transform.rotation);
            }            
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

    // staggering podia ser um script tb se quisessemos por enemies sem stigger
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
