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
        
    }
}
