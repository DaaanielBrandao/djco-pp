using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // units/s
    public float maxTime; // s
    public float damage; // HP

    private GameObject shooter;

    private float dir;
    protected Vector2 charSpeed;
    protected float charge;

    // Start is called before the first frame update
    void Start()
    {        
        dir = shooter.GetComponent<CharacterMovement>().facingDir.x;
        var localScale = transform.localScale;
        localScale = new Vector2(dir * Mathf.Abs(localScale.x), localScale.y);
        transform.localScale = localScale;

        charSpeed = new Vector2(
            shooter.GetComponent<Rigidbody2D>().velocity.x,
            shooter.GetComponent<Rigidbody2D>().velocity.y
        );
        onStartOther();
        StartCoroutine(DestroyAfterLifetime());
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate((Vector2.right * dir * speed + charSpeed) * Time.deltaTime);
    }

    IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        int layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("Ground"))
        {
            if (layer == LayerMask.NameToLayer("Enemy"))
                other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
            Destroy(gameObject);
        }
    }

    protected virtual void onStartOther()
    {
        
    }

    public static void SpawnBullet(GameObject bullet, GameObject shooter, Vector3 position, Quaternion rotation, float charge = 1) {
        GameObject obj = Instantiate(bullet, position, rotation);
        obj.GetComponent<Bullet>().shooter = shooter;
        obj.GetComponent<Bullet>().charge = charge;
    }
}
