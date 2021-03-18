using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponSemiAuto
{

    public GameObject bullets;
    public GameObject scope;
    public float laserRange = 300f;
    private LineRenderer lineRenderer;
    
    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected void Update()
    {
        base.Update();
        
        if (canShoot)
        {
            lineRenderer.enabled = true;
            Vector2 origin = scope.transform.position;
            Vector2 dir = new Vector2(shooter.GetComponent<CharacterMovement>().facingDir.x, 0);
            RaycastHit2D groundHit = Physics2D.Raycast(origin, dir, laserRange, LayerMask.GetMask("Ground", "Enemy"));
            if (groundHit)
                RenderLineTo(groundHit.point);
            else RenderLineTo(origin + dir * laserRange);
        }
        else lineRenderer.enabled = false;
    }

    protected override void Shoot()
    {
        Bullet.SpawnBullet(bullets, shooter, hole.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    
    private void RenderLineTo(Vector2 to) {
        lineRenderer.SetPosition(0, scope.transform.position);
        lineRenderer.SetPosition(1, to);
    }
    
}
