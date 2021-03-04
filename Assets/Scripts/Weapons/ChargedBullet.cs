using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet
{
    public float minSpeed;
    public float minDamage;
    
    protected override void onStartOther()
    {
        //speed
        speed = minSpeed + charge * (speed - minSpeed);
        
        //size
        Vector3 localScale = transform.localScale;
        localScale = new Vector3(localScale.x * (1 + charge) , localScale.y * (1 + charge) , localScale.z);
        transform.localScale = localScale;
        
        //damage
        damage = minDamage + charge * (damage - minDamage);
        
        
        charSpeed = new Vector2(charSpeed.x, 0);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (charge < 1)
            base.OnTriggerEnter2D(other);
        else
        {
            int layer = other.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Ground"))
                Destroy(gameObject);
            else if (layer == LayerMask.NameToLayer("Enemy"))
                other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
        }
    }
}
