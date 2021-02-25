using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public float attackCooldown = 1f;
    public float visionRadius = 50f;
    
    public GameObject bullet; 

    private PlayerDetector detector;
    private Animator animator;
  
    // Start is called before the first frame update
    void Awake() {
       animator = GetComponent<Animator>();
       detector = gameObject.AddComponent<PlayerDetector>();
       detector.radius = visionRadius;
    }

    private void OnEnable() {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator Attack() {        
        while (true) {
            GameObject follow = detector.nearestPlayer;

            if (follow != null && Vector2.Distance(follow.transform.position, transform.position) <= visionRadius) {
                animator.SetTrigger("shoot");
                yield return new WaitForSeconds(1f);

                if (this.enabled) {
                    Vector3 dir = follow.transform.position - transform.position;
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
                }
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }

}
