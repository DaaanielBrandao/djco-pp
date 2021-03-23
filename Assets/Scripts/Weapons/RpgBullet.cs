using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgBullet : Bullet
{
	public float explosionDamage = 50.0f;
	public float explosionRange = 10.0f;
	
	public float playerDamage = 50.0f;
	public float playerKnockback = 50.0f;
	
	
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

		Collider2D[] enemyHits = Physics2D.OverlapCircleAll(transform.position, explosionRange, LayerMask.GetMask("Enemy"));
		foreach (Collider2D hit in enemyHits) {
			hit.gameObject.GetComponent<EnemyHP>().OnHit(explosionDamage);
		}
		
		Collider2D[] playerHits = Physics2D.OverlapCircleAll(transform.position, explosionRange, LayerMask.GetMask("Player"));
		foreach (Collider2D hit in playerHits) {
			hit.gameObject.GetComponent<PlayerHP>().OnHit(playerDamage);
			hit.gameObject.GetComponent<Rigidbody2D>().velocity = playerKnockback * (hit.transform.position - transform.position).normalized;
		}
		SoundManager.Instance.Play(kaboomSound);
		Instantiate(explosionPS, transform.position, explosionPS.transform.rotation);
	}
	
	protected override IEnumerator DestroyAfterLifetime() {
		yield return new WaitForSeconds(maxTime);
		Explode();
		Destroy(gameObject);
	}
}