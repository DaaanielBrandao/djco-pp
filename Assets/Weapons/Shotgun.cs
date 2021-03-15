﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponSemiAuto
{
    public float spreadAngle = 40;
    public int numBullets = 6;

    public GameObject bullets;

    protected override void Shoot() {
        /*
        Rigidbody2D rbCharacter = GameObject.Find("Character").GetComponent<Rigidbody2D>();
        int dir = -GameObject.Find("Character").GetComponent<CharacterMovement>().turned;

        rbCharacter.AddForce(Vector2.right * dir * 20, ForceMode2D.Impulse);
        */

        for(int i = 0; i < numBullets; i++){
            float angle = Random.Range(-spreadAngle/2,spreadAngle/2);
            Bullet.SpawnBullet(bullets, shooter, hole.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        }

        GameObject.FindObjectOfType<Camera>().GetComponent<Animator>().SetTrigger("shake"); //é capaz de ficar melhor qnd se mata alguem
    }
}