using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class HookBullet : Bullet
{

	private GameObject pulledEnemy;
	private LineRenderer lineRenderer;
	
	public float pullSpeed = 2f;
	public float maxDistance = 5.0f;

	private Vector3 relativeHolePosition;

	protected override void Start()
	{
		base.Start();

		lineRenderer = GetComponent<LineRenderer>();
		relativeHolePosition = transform.position - shooter.transform.position;
	}
	
	protected override void Update()
	{
		if (!pulledEnemy)
			base.Update();
		else
		{
			Vector2 dir = shooter.transform.position - pulledEnemy.transform.position;
	
			if (dir.magnitude > maxDistance)
				pulledEnemy.transform.Translate(dir * (pullSpeed * Time.deltaTime));
			else Destroy(gameObject);
		}
		
		lineRenderer.SetPosition(0, shooter.transform.position + relativeHolePosition);
		lineRenderer.SetPosition(1, transform.position);
	}

	protected override void OnEnemyEnter(Collider2D other)
	{
		if (!pulledEnemy)
		{
			pulledEnemy = other.gameObject;
			other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
		}
	}
}