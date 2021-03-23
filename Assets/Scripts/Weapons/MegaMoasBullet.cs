using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMoasBullet : Bullet
{
	public bool isStuck = false;
	private Transform stuckTo;
	
	private Vector3 stuckPosOffset;
	private Quaternion stuckRotOffset;
	
	protected override void OnEnemyEnter(Collider2D other)
	{
		other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
		StickTo(other.gameObject.transform);
	}
	
	protected override void OnGroundEnter(Collider2D other)
	{
		StickTo(other.gameObject.transform);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		if (!isStuck)
			base.Update();
	}

	private void StickTo(Transform surfaceTransform)
	{
		isStuck = true;

		transform.parent = surfaceTransform;
	}
}


