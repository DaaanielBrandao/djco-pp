using System.Collections;
using UnityEngine;
public class Laser : WeaponAuto
{
	public float range = 50f;
	public float damage = 20;
	public ParticleSystem laserPS;
	
	private LineRenderer lineRenderer;
	
	private Collider2D currentEnemy;
	

	protected void Start()
	{
		base.Start();
		lineRenderer = GetComponent<LineRenderer>();
	}

	protected void Update()
	{
		base.Update();


		//Vector2 shooterVelocity = shooter.GetComponent<Rigidbody2D>().velocity;
		//float angle = Vector2.Angle(shooterVelocity, Vector2.right);

		Vector2 origin = hole.transform.position;
		Vector2 dir = new Vector2(shooter.GetComponent<CharacterMovement>().facingDir.x, 0);

		Debug.Log(currentEnemy);
		if (IsShooting()) {
			laserPS.Play();
			if (currentEnemy)
			{
				Vector2 enemyDir = (Vector2) currentEnemy.bounds.center - origin;
				float angle = Vector2.Angle(dir, enemyDir);
				RaycastHit2D groundHit =
					Physics2D.Raycast(origin, dir, enemyDir.magnitude, LayerMask.GetMask("Ground"));

				if (groundHit || angle > 60)
					currentEnemy = null;
				else
				{
					RenderLineTo(currentEnemy.bounds.center);
					laserPS.transform.rotation = Quaternion.LookRotation(enemyDir);
				}
			}
			else {
				RaycastHit2D groundHit = Physics2D.Raycast(origin, dir, range, LayerMask.GetMask("Ground"));

				if (groundHit)
					RenderLineTo(groundHit.point);
				else RenderLineTo(origin + dir * range);
				laserPS.transform.rotation = Quaternion.Euler(dir.x >= 0 ? 0 : 180 ,90, 0);
			}
		}
		else
		{
			laserPS.Stop();
		}
		
		lineRenderer.enabled = IsShooting();
		GetComponent<Animator>().SetBool("pewing",IsShooting());
	}

	private void RenderLineTo(Vector2 to) {
		lineRenderer.SetPosition(0, hole.transform.position);
		lineRenderer.SetPosition(1, to);
	}
	
	
	protected override void Shoot()
	{
		if (!currentEnemy) {
			Vector2 origin = hole.transform.position;
			Vector2 dir = new Vector2(shooter.GetComponent<CharacterMovement>().facingDir.x, 0);
			currentEnemy = GetClosestHittableEnemy(origin, dir);
		}
		if (currentEnemy)
			currentEnemy.GetComponent<EnemyHP>().OnHit(damage);
	}

	private Collider2D GetClosestHittableEnemy(Vector2 origin, Vector2 playerDir)
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(origin, range, LayerMask.GetMask("Enemy"));

		Collider2D closest = null;
		float minDistance = Mathf.Infinity;
		
		foreach (Collider2D hit in hits)
		{
			Vector2 hitDir = (Vector2)hit.bounds.center - origin;
			float angle = Vector2.Angle(hitDir, playerDir);
			float distance = hitDir.magnitude;
			
			RaycastHit2D ground = Physics2D.Raycast(origin, hitDir, distance, LayerMask.GetMask("Ground"));
			
			if (!ground && angle < 45 && distance < minDistance) {
				minDistance = distance;
				closest = hit;
			}
		}

		return closest;
	}
}