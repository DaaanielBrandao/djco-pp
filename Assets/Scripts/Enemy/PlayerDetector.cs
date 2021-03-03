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
            if (hit != null)
                nearestPlayer = hit.gameObject;

            yield return new WaitForSeconds(cooldown);
        }
    }
}
