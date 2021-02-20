using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet {

    public void Reset() {
        speed = 70f;
        maxTime = 0.01f;
    }
}   
