using UnityEngine;

public class GroundFinder {
	public static RaycastHit2D RayCast(Vector2 position, Vector2 direction, float distance)
	{
		Physics2D.queriesHitTriggers = false;
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, LayerMask.GetMask("Ground", "Platform"));
		Physics2D.queriesHitTriggers = true;

		return hit;
	}

	public static GameObject SpawnOnGround(GameObject obj, Vector2 airPosition)
	{
		GameObject createdObj = Object.Instantiate(obj, airPosition, obj.transform.rotation);

		// Find ground below and move ammo box just over it
		RaycastHit2D hit = RayCast(airPosition, Vector2.down, Mathf.Infinity);
		Vector2 ammoBoxColliderExtents = createdObj.GetComponent<Collider2D>().bounds.extents;

		createdObj.transform.position = hit.centroid + new Vector2(0, ammoBoxColliderExtents.y);

		return createdObj;
	}
}