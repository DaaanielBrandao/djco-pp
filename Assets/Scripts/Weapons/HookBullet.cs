﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class HookBullet : Bullet
{

	private GameObject pulledEnemy;
	private LineRenderer renderer;
	
	public float pullSpeed = 2f;
	public float maxDistance = 5.0f;

	protected void Start()
	{
		base.Start();

		renderer = GetComponent<LineRenderer>();
	}
	protected void Update()
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
		
		renderer.SetPosition(0, shooter.transform.position);
		renderer.SetPosition(1, transform.position);
	}

	protected override void OnEnemyEnter(Collider2D other)
	{
		pulledEnemy = other.gameObject;
		other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
	}
}