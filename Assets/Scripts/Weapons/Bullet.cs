using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // units/s
    public float maxTime; // s
    public float damage; // HP

    public GameObject shooter;

    private float dir;
    private Vector2 charSpeed;

    // Start is called before the first frame update
    void Start()
    {        
        dir = shooter.GetComponent<CharacterMovement>().facingDir.x;
        transform.localScale = new Vector2(dir * Mathf.Abs(transform.localScale.x), transform.localScale.y);

        charSpeed = new Vector2(
            shooter.GetComponent<Rigidbody2D>().velocity.x,
            shooter.GetComponent<Rigidbody2D>().velocity.y
        );

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

    private void OnTriggerEnter2D(Collider2D other) {
        int layer = other.gameObject.layer;
        if (layer != LayerMask.NameToLayer("Player")) {
            if (layer == LayerMask.NameToLayer("Enemy"))
                other.gameObject.GetComponent<EnemyHP>().OnHit(damage);
            Destroy(gameObject);
        }
    }

    public static void SpawnBullet(GameObject bullet, GameObject shooter, Vector3 position, Quaternion rotation) {
        GameObject obj = Instantiate(bullet, position, rotation);
        obj.GetComponent<Bullet>().shooter = shooter;
    }
}
