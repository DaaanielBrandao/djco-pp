using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : Weapon
{
    protected override void OnShoot() {
        Instantiate(bullets, hole.transform.position,transform.rotation);
    }
}
