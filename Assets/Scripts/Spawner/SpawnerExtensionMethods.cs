using UnityEngine;

public static class SpawnerExtensionMethods {
	public static Vector2 GetValidSpawnPosition(this Collider2D[] colliders, float minPlayerDistance = 30.0f) {
		// Gets a random index and goes on until it finds a collider where no player is nearby
		int index = Random.Range(0, colliders.Length);

		for (int i = index; i < colliders.Length; i++) {
			Vector3 randomPos = colliders[i].GetRandomPos();
			if (!Physics2D.OverlapCircle(randomPos, minPlayerDistance, LayerMask.GetMask("Player")))
				return randomPos;
		}
		
		for (int i = 0; i < index; i++) {
			Vector3 randomPos = colliders[i].GetRandomPos();
			if (!Physics2D.OverlapCircle(randomPos, minPlayerDistance, LayerMask.GetMask("Player")))
				return randomPos;
		}
		
		// If there are players on all colliders, just get the original one
		Debug.Log("None of these are valid positions! :(");
		return colliders[index].GetRandomPos();
	}

	public static Vector2 GetRandomPos(this Collider2D collider)
	{
		Bounds bounds = collider.bounds;
		return new Vector2(Random.Range(bounds.min.x, bounds.max.x),Random.Range(bounds.min.y, bounds.max.y));
	}
}