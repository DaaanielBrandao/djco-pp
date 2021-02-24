using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public float attackCooldown = 1f;
    
    public GameObject bullet; // DONI É TEMPORARIO

    private Animator animator;
    private GameObject currentFollow;
    public float visionRadius = 50f;
  
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        currentFollow = GameObject.Find("Character"); // !!!
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Attack() {        
        while (true) {
            Vector3 dir = currentFollow.transform.position - transform.position;

            if (dir.magnitude <= visionRadius) {
                Debug.Log("Im attacking you bro!");
              

                Instantiate(bullet, transform.position + dir.normalized * 5, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }

}
