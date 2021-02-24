using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackCooldown = 1f;    
    
    public float staggerHP = 0.25f;
    public float staggerTime = 5f;
    public float visionRadius = 50f;

    private FloatingEnemy movementBehavior; // beep boop moas
    private EnemyHP enemyHP;
    private Animator animator;
    private GameObject follow;

    public ParticleSystem explosionEffect;

    public enum EnemyState {NotSee = 0, Ready = 1, Staggered = 2, Dying}
    public EnemyState enemyState = EnemyState.NotSee; 



    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindGameObjectWithTag("Player");
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();        
        movementBehavior = GetComponent<FloatingEnemy>();
        movementBehavior.SetFollow(follow); // moas beep boop
    }

    private void OnEnable() {
        Debug.Log("hi"); 
        StartCoroutine(Attack());     
    }


    IEnumerator Attack() {
        
        while (true) { // moas beep boop ?
            if (enemyState == EnemyState.Ready) {
                Debug.Log("Im attacking!");
                animator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    
    IEnumerator Die() { // moas beep boop ?
        enemyState = EnemyState.Dying;
        animator.SetTrigger("Dying");
        yield return new WaitForSeconds(0.03f);
        SoundManager.Instance.OnEnemyExplosion();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Falta o dash!!
        Debug.Log(enemyState);

        if (enemyState != EnemyState.Dying && enemyHP.currentHP <= 0) {
            StartCoroutine(Die());
        }
        else if (enemyState != EnemyState.Dying && enemyHP.percentageOfMax() < staggerHP) {
            enemyState = EnemyState.Staggered;
        }
        else switch (enemyState) {
            case EnemyState.NotSee:
                if (Vector2.Distance(follow.transform.position, transform.position) <= visionRadius)
                    enemyState = EnemyState.Ready;
                break;
            case EnemyState.Ready:
                if (Vector2.Distance(follow.transform.position, transform.position) > visionRadius)
                    enemyState = EnemyState.NotSee;
                break;
            default: break;
        }

        movementBehavior.enabled = enemyState == EnemyState.Ready;
        animator.SetInteger("State", (int)enemyState);
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(enemyState != EnemyState.Dying && other.gameObject.CompareTag("Player Bullet")) {
            enemyHP.changeHP(-1);
        }
    }
}
