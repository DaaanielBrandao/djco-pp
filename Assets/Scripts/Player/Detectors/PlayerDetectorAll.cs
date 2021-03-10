using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
public class PlayerDetectorAll : MonoBehaviour
{
    public float cooldown = 1;
    public float radius = 50;

    public GameObject[] nearbyPlayers = {};
    
    void OnEnable() {
        StartCoroutine(Detect());
    }

    IEnumerator Detect() {
        while (true) {            
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask(new string[]{"Player"}));

            nearbyPlayers = hits.Select(hit => hit.gameObject).ToArray();


            yield return new WaitForSeconds(cooldown);
        }
    }
}
*/