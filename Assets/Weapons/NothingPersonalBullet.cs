using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class NothingPersonalBullet : Bullet {
	// private bool dashing;
	// private GameObject enemy;
    // protected void Update() {
	//     
	//     base.Update();
	//     if (dashing) {
	// 	    
	// 	    CharacterMovement cm = shooter.GetComponent<CharacterMovement>();
	// 	 
	// 	    if (!enemy) {
	// 		    cm.dashState = CharacterMovement.DashState.Ready;
	// 		    Destroy(gameObject);
	// 		    return;
	// 	    }
	// 	    
	// 	    Vector2 dir = enemy.transform.position - shooter.transform.position;
	// 	    if (dir.magnitude > 1.0)
	// 	    {
	// 		    cm.dashSpeed = 150;
	// 		    cm.dashState = CharacterMovement.DashState.Dashing;
	// 		    cm.dashDir = dir.normalized;
	// 	    }
	// 	    else {
	// 		    cm.dashState = CharacterMovement.DashState.Ready;
	// 		    Destroy(gameObject);
	// 	    }
	//     }
    // }
    protected override void OnEnemyEnter(Collider2D other) {
		Transform st = shooter.transform;
		st.position = other.gameObject.transform.position + transform.right * 5 + new Vector3(0, 5, 0);

		shooter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		
		base.OnEnemyEnter(other);

		//if (!enemy)
		//{
		//	dashing = true;
		//	enemy = other.gameObject;
		//	other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
		//}
	}
}