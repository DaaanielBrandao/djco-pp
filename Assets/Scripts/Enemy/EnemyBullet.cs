using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 30; // units/s
    public float maxTime = 5; // s
    public float damage = 100; // HP

    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(DestroyAfterLifetime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
}
