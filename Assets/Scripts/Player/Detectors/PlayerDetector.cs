using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public float cooldown = 1;
    public float radius = 50;

    public GameObject nearestPlayer = null;
    
    void OnEnable() {
        StartCoroutine(Detect());
    }

    IEnumerator Detect() {
        while (true) {            
            Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask(new string[]{"Player"}));
            if (hit)
                nearestPlayer = hit.gameObject;

            yield return new WaitForSeconds(cooldown);
        }
    }
    
    public static GameObject GetPlayerCollision(Collider2D collider) {
        Collider2D[] collisions = new Collider2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Player"));
        if (collider.OverlapCollider(filter, collisions) > 0)
            return collisions[0].gameObject;     
        return null;
    }
}
