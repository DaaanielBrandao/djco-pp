﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {
    
    public void Reset() {
        speed = 60f;
        maxTime = 0.6f;
    }
}
