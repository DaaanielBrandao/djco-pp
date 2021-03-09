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

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity,
				LayerMask.GetMask("Ground"));
			if (hit) {
				transform.right = Vector3.Reflect(transform.right,  hit.normal);

				SoundManager.Instance.Play(boingSound);
			}
		}
		else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
			Destroy(gameObject);
		}
	}
}