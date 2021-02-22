using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed = 60f;
    public float maxTime = 0.6f;

    private float dir;
    private Vector2 charSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject character = GameObject.Find("Character");
        
        dir = character.GetComponent<CharacterMovement>().facingDir.x;

        charSpeed = new Vector2(
            character.GetComponent<Rigidbody2D>().velocity.x,
            character.GetComponent<Rigidbody2D>().velocity.y
        );

        StartCoroutine(DestroyAfterLifetime());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((Vector2.right * dir * speed + charSpeed) * Time.deltaTime);

        transform.Translate((Vector2.right * dir * speed + charSpeed) * Time.deltaTime);
    }

    IEnumerator DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer != PlatformCollision.PlatformLayer && !other.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }
}
