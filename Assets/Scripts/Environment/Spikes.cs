using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 4;
    // Start is called before the first frame update
    public float knockbackForce = 32f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            CharacterMovement cm = other.gameObject.GetComponent<CharacterMovement>();
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 knockbackDir = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            playerRb.velocity = knockbackDir * knockbackForce;
            other.gameObject.GetComponent<PlayerHP>().OnHit(damage);

            cm.Boioioing();
        }
    }
}
