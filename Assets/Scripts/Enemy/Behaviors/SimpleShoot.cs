using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : AttackBehavior
{
    public enum ShootType { Targeted, Auto, Forward };
    public ShootType shootType = ShootType.Targeted;

    public float cooldown = 1f;
    public int numShots = 1;
    public float angle = 0;
    public float visionRadius = 50f;
    public float defaultAngle = 0;
    public GameObject bullet; 

    private IEnumerator coroutine;
    private PlayerDetector detector;
    private Animator animator;
  
    // Start is called before the first frame update
    void Awake() {
        animator = GetComponent<Animator>();
        detector = gameObject.AddComponent<PlayerDetector>();
        detector.radius = visionRadius;
    }

    private void OnEnable() {
        coroutine = Attack();
        StartCoroutine(coroutine);
    }

    private void OnDisable() {
        StopCoroutine(coroutine);
    }

    private bool CanShoot() {
        if (shootType != ShootType.Targeted)
            return true;
            
        GameObject follow = detector.nearestPlayer;
        return follow != null && Vector2.Distance(follow.transform.position, transform.position) <= visionRadius;
    }

    private Vector3 GetDefaultRotation() {
        switch (shootType)
        {  
            case ShootType.Targeted:
                GameObject follow = detector.nearestPlayer;
                if (follow == null)
                    return new Vector3(0, 0, defaultAngle);
                Vector3 dir = follow.transform.position - transform.position;
                return new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + defaultAngle);
            case ShootType.Auto:
                return new Vector3(0, 0, defaultAngle);
            case ShootType.Forward:
                return new Vector3(0, 0, transform.localScale.x < 0 ? 180 : 0); // 2 directions    
            default: Debug.LogError("Unhandled shoot type: " + shootType); return Vector3.zero;
        }
    }

    IEnumerator Attack() {       
        while (true) {

            if (CanShoot()) {
                animator.SetTrigger("shoot");
                yield return new WaitForSeconds(1f);

                Vector3 rotation = GetDefaultRotation();

                for(int i = 0; i < numShots; i++) {

                    float bulletAngle = angle * (i - (numShots - 1) / 2.0f);

                    Instantiate(bullet, transform.position, Quaternion.Euler(rotation + new Vector3(0, 0, bulletAngle)));
                }

            }

            yield return new WaitForSeconds(cooldown);
        }
    }
}
