using UnityEngine;

public class Powerup : MonoBehaviour
{
	public string powerUpName;
	
	
	public Sprite hatSprite;
	public Material hatMaterial;
	
	public Color particleColor;

	public float dashSpeed;

	public float duration;

	public void Explode()
	{
		Destroy(gameObject);
		// PS?
	}
}