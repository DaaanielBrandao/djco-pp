using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : Weapon
{   
    public float spreadAngle = 5;
    protected override void OnShoot() {
        float angle = Random.Range(-spreadAngle/2,spreadAngle/2);


        Instantiate(bullets, hole.transform.position,new Quaternion(90,angle,0,0));

        SoundManager.Instance.OnShootPistol();
    }
}
