using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    private float spreadAngle = 40;
    private int numBullets = 6;

    protected override void OnShoot() {
        for(int i = 0; i < numBullets; i++){
            float angle = Random.Range(-spreadAngle/2,spreadAngle/2);
            Instantiate(bullets,hole.transform.position, new Quaternion(90,angle,0,0));
        }
    }
}
