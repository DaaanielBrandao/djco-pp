using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    protected override void OnEnemyEnter(Collider2D other) {
        other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
        if(hitEnemyPS != null)
            Instantiate(hitEnemyPS, transform.position, transform.rotation);
    }
}
