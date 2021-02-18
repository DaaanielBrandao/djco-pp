using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 60f;
    protected float maxTime = 0.6f;

    private float dir;
    private Vector3 charSpeed;

    // Start is called before the first frame update
    void Start()
    {
        dir = GameObject.Find("Character").GetComponent<CharacterMovement>().facingDir.x;
        charSpeed = GameObject.Find("Character").GetComponent<CharacterMovement>().movement;

        StartCoroutine(DestroyAfterLifetime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * dir * Time.deltaTime * speed + charSpeed);
    }

    IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }
}
