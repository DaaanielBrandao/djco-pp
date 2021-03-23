using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMoasWeapon : WeaponSemiAuto
{
    public GameObject bulletPrefab;
    public float activationDamage = 50;
    
    public AudioClip activationSound;

    private GameObject firstMine;
    private GameObject secondMine;
    
    private LineRenderer lineRenderer;

    private List<Tuple<GameObject, GameObject, bool>> awaitingActivation = new List<Tuple<GameObject, GameObject, bool>>();

    private void OnDisable()
    {
        if (firstMine)
            Destroy(firstMine);
        if (secondMine)
            Destroy(secondMine);
        foreach (var pair in awaitingActivation)
        {
            Destroy(pair.Item1);
            Destroy(pair.Item2);
        }
        awaitingActivation.Clear();
    }

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    protected override void Update() {
        base.Update();

        if (firstMine && secondMine) {
            // StartCoroutine(ScheduleActivation(firstMine, secondMine));
            awaitingActivation.Add(new Tuple<GameObject, GameObject, bool>(firstMine, secondMine, false));
            firstMine = null;
            secondMine = null;
        }

        for (int i = 0; i < awaitingActivation.Count; i++)
        {
            var (item1, item2, isScheduled) = awaitingActivation[i];
            if (item1 && item2)
            {
                if (isScheduled)
                    continue;
                MegaMoasBullet mine1 = item1.GetComponent<MegaMoasBullet>();
                MegaMoasBullet mine2 = item2.GetComponent<MegaMoasBullet>();
                if (mine1.isStuck && mine2.isStuck)
                {
                    awaitingActivation[i] = new Tuple<GameObject, GameObject, bool>(item1, item2, true);
                    StartCoroutine(ScheduleActivation(item1, item2));
                }
            }
            else {
                Destroy(item1);
                Destroy(item2);
                awaitingActivation.RemoveAt(i--);
            }
        }
        
    }

    protected override void Shoot()
    {
        GameObject bullet = Bullet.SpawnBullet(bulletPrefab, shooter, hole.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (firstMine)
            secondMine = bullet;
        else firstMine = bullet;
    }

    IEnumerator ScheduleActivation(GameObject mine1, GameObject mine2)
    {
        yield return new WaitForSeconds(0.5f);
        if (!mine1 || !mine2) {
            Destroy(mine1);
            Destroy(mine2);          
            yield break;
        }
        
        Vector2 from = mine1.transform.position, to = mine2.transform.position;
        Vector2 dir = to - from;
        float distance = dir.magnitude;

        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
        lineRenderer.enabled = true;

        RaycastHit2D[] lineHits = Physics2D.CircleCastAll(from, 2.0f, dir, distance, LayerMask.GetMask("Enemy"));
        foreach (var enemyHit in lineHits)
            enemyHit.collider.GetComponent<EnemyHP>().OnHit(activationDamage);
                
        SoundManager.Instance.Play(activationSound);
                    
                  
        Destroy(mine1);
        Destroy(mine2);
        
        yield return new WaitForSeconds(0.4f);
        lineRenderer.enabled = false;
    }
}
