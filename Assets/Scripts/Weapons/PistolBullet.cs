using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {
    
    PistolBullet() {
        speed = 80f;
        maxTime = 0.6f;
        damage = 30;
    }
}
