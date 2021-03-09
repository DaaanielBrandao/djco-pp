using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class NothingPersonalBullet : Bullet
{
	protected override void OnEnemyEnter(Collider2D other) {
		Transform st = shooter.transform;
		st.position = other.gameObject.transform.position + transform.right * 5 + new Vector3(0, 5, 0);

		shooter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		
		base.OnEnemyEnter(other);
	}
}