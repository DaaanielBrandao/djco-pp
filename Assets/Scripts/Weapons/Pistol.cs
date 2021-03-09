using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : WeaponSemiAuto
{   
    public float minAngle = -2.5f;
    public float maxAngle = 2.5f;

    public GameObject bullets;

    protected override void Shoot()
    {
        float angle = Random.Range(minAngle, maxAngle);

        Bullet.SpawnBullet(bullets, shooter, hole.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
