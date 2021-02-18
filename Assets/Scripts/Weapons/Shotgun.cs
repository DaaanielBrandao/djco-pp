using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public float spreadAngle = 40;
    public int numBullets = 6;

    protected override void OnShoot() {
        /*
        Rigidbody2D rbCharacter = GameObject.Find("Character").GetComponent<Rigidbody2D>();
        int dir = -GameObject.Find("Character").GetComponent<CharacterMovement>().turned;

        rbCharacter.AddForce(Vector2.right * dir * 20, ForceMode2D.Impulse);
        */

        for(int i = 0; i < numBullets; i++){
            float angle = Random.Range(-spreadAngle/2,spreadAngle/2);
            Instantiate(bullets,hole.transform.position, new Quaternion(90,angle,0,0));
        }
    }
}
