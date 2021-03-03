using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : Weapon
{   
    public float spreadAngle = 5;
    protected override void OnShoot() {
        float angle = Random.Range(-spreadAngle/2,spreadAngle/2);

        SpawnBullet(hole.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
