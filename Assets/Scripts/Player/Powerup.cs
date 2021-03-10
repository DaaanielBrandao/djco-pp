using UnityEngine;

public class Powerup : MonoBehaviour
{
	public string powerUpName;
	
	
	public Sprite hatSprite;
	
	public Color particleColor;

	public float duration;

	public void Explode()
	{
		Destroy(gameObject);
		// PS?
	}
}