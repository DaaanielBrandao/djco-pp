using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : WeaponSemiAuto
{   
    public float spreadAngle = 5;

    public GameObject bullets;

    protected override void Shoot() {
        float angle = Random.Range(-spreadAngle/2, spreadAngle/2);

        Bullet.SpawnBullet(bullets, shooter, hole.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
