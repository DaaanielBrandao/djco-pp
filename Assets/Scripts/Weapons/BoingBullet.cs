using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoingBullet : Bullet
{

	public AudioClip boingSound;

	protected void Start() {
		
		base.Start();

		charSpeed = Vector2.zero;
	}

	protected override void OnGroundEnter(Collider2D other)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity,
			LayerMask.GetMask("Ground"));
		if (hit) {
			transform.right = Vector3.Reflect(transform.right,  hit.normal);

			SoundManager.Instance.Play(boingSound);
			if(hitWallPS != null)
				Instantiate(hitWallPS, transform.position, transform.rotation);
		}
	}
}