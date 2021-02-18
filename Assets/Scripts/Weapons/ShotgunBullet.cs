using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet {

    public void Reset() {
        speed = 50f;
        maxTime = 0.3f;
    }
}   
