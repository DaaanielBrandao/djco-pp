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
            Collider2D[] list = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask(new string[]{"Character"}));
            foreach (Collider2D collider in list) {
                if (collider.CompareTag("Player")) {
                    nearestPlayer = collider.gameObject;
                    break;
                }
            }

            yield return new WaitForSeconds(cooldown);
        }
    }
}
