using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienEngine : WeaponAuto
{
    public float spreadAngle = 0;
    public float radius = 3;
    public float distance = 2;
    public float damage = 20;
    public float force = 1000f;

    public GameObject bullets;
    protected override void Shoot() {
        float angle = Random.Range(-spreadAngle/2, spreadAngle/2);

        hitboxar();
        shooter.GetComponent<Rigidbody2D>().AddForce(Vector3.right * (-shooter.GetComponent<CharacterMovement>().facingDir.x * Time.deltaTime * force), ForceMode2D.Impulse);
    }

    private void hitboxar()
    {
        LayerMask mask = LayerMask.GetMask(new string[]{"Enemy"});
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(hole.transform.position.x, hole.transform.position.y), radius, new Vector2(shooter.GetComponent<CharacterMovement>().facingDir.x,0), distance, mask);
        foreach (RaycastHit2D hit in hits) {
            hit.collider.gameObject.GetComponent<EnemyHP>().OnHit(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 otherCenter = new Vector3(hole.transform.position.x + distance * shooter.GetComponent<CharacterMovement>().facingDir.x,hole.transform.position.y,hole.transform.position.z);
        Gizmos.DrawWireSphere(hole.transform.position,radius);
        Gizmos.DrawLine(hole.transform.position,otherCenter);
        Gizmos.DrawWireSphere(otherCenter,radius);
    }
}
