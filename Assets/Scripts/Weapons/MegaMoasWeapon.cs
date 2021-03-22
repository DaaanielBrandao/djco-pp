using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMoasWeapon : WeaponSemiAuto
{
    public GameObject minePrefab;
    public float damage = 50;
    
    public AudioClip activationSound;

    private GameObject firstMine;
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Shoot()
    {

        Vector2 shooterDir = shooter.GetComponent<CharacterMovement>().facingDir;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shooterDir, Mathf.Infinity, LayerMask.GetMask("Ground"));

        Debug.Log(transform.position + " " + shooterDir);
        if (hit.collider) {
            Debug.Log("Bazng");
            GameObject newMine = Instantiate(minePrefab, hit.point, minePrefab.transform.rotation);
            if (firstMine)
            {
                StartCoroutine(ScheduleActivation(firstMine, newMine));
                firstMine = null;
            }
            else firstMine = newMine;
        }

    }

    IEnumerator ScheduleActivation(GameObject mine1, GameObject mine2)
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 from = mine1.transform.position, to = mine2.transform.position;

        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
        lineRenderer.enabled = true;

        RaycastHit2D[] lineHits = Physics2D.LinecastAll(from, to, LayerMask.GetMask("Enemy"));
        foreach (var enemyHit in lineHits)
            enemyHit.collider.GetComponent<EnemyHP>().OnHit(damage);
                
        SoundManager.Instance.Play(activationSound);
                    
        yield return new WaitForSeconds(0.5f);
           lineRenderer.enabled = false;
           
        Destroy(mine1);
        Destroy(mine2);
    }
}
