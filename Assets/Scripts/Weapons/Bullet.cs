using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // units/s
    public float maxTime; // s
    public float damage; // HP
    public ParticleSystem hitWallPS;
    public ParticleSystem hitEnemyPS;

    protected GameObject shooter;

    protected Vector2 charSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {        

        Vector2 velocity = shooter.GetComponent<Rigidbody2D>().velocity;

        if (shooter.GetComponent<CharacterMovement>().facingDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180 - transform.rotation.eulerAngles.z);
            charSpeed = new Vector2(-velocity.x, -velocity.y);
        }
        else charSpeed = new Vector2(velocity.x, velocity.y);


        
        StartCoroutine(DestroyAfterLifetime());
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        transform.Translate((Vector2.right * speed + charSpeed) * Time.deltaTime);
    }

    IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        int layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("Ground")) {
            if (layer == LayerMask.NameToLayer("Enemy"))
                OnEnemyEnter(other);
            else OnGroundEnter(other);
        }
    }

    protected virtual void OnGroundEnter(Collider2D other) {
        if(hitWallPS != null)
            Instantiate(hitWallPS, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    protected virtual void OnEnemyEnter(Collider2D other) {
        other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
        if(hitEnemyPS != null)
            Instantiate(hitEnemyPS, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public static GameObject SpawnBullet(GameObject bullet, GameObject shooter, Vector3 position, Quaternion rotation) {
        GameObject obj = Instantiate(bullet, position, rotation);
        obj.GetComponent<Bullet>().shooter = shooter;
        return obj;
    }
}
