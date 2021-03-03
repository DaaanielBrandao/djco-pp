using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HealthBar
{    
        // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHP);
    }

    public override void Die() {
        Debug.Log("Player DED");
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy Bullet")) {  
            Debug.Log("bang bang");
            float damage = other.gameObject.GetComponent<EnemyBullet>().damage;
            this.changeHP(-damage);
            Destroy(other.gameObject);
        }
    }
}
