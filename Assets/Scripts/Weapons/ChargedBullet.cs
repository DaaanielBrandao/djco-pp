using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : Bullet
{
    protected float charge;
    
    public float minSpeed;
    public float minDamage;
    
    protected void Start()
    {
        base.Start();
            
        //speed
        speed = minSpeed + charge * (speed - minSpeed);
        
        //size
        transform.localScale += new Vector3(charge, charge, 0);
        
        //damage
        damage = minDamage + charge * (damage - minDamage);
        
        
        charSpeed = new Vector2(charSpeed.x, 0);
    }

    
    protected override void OnEnemyEnter(Collider2D other) {
        if (charge < 1)
            base.OnEnemyEnter(other);
        else other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
    }

    public static GameObject SpawnChargedBullet(GameObject bullet, GameObject shooter, Vector3 position, Quaternion rotation, float charge)
    {
        GameObject obj = SpawnBullet(bullet, shooter, position, rotation);
        obj.GetComponent<ChargedBullet>().charge = charge;
        return obj;
    }
}
