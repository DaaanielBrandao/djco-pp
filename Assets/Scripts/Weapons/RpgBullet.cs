using UnityEngine;

public class RpgBullet : Bullet
{
	public float explosionDamage = 50.0f;
	public float explosionRange = 10.0f;
	public ParticleSystem explosionPS;
	
	public AudioClip kaboomSound;


	protected override void Start()
	{
		base.Start();
		
		Debug.Log(shooter);
	}

	protected override void OnEnemyEnter(Collider2D other)
	{
		base.OnEnemyEnter(other);
		Explode();
	}
	
	protected override void OnGroundEnter(Collider2D other)
	{
		base.OnGroundEnter(other);
		Explode();
	}

	private void Explode()
	{

		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange, LayerMask.GetMask("Enemy"));
		Debug.Log(hits.Length);
		foreach (Collider2D hit in hits) {
			hit.gameObject.GetComponent<EnemyHP>().OnHit(explosionDamage);
		}
		
		SoundManager.Instance.Play(kaboomSound);
		Instantiate(explosionPS, transform.position, explosionPS.transform.rotation);
	}
}