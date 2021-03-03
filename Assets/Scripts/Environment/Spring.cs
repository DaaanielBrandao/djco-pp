using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 60f;
    //public float minJumpSpeed = 20f;
    //public float maxJumpSpeed = 80f;

    public AudioClip boingSound;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            CharacterMovement cm = other.gameObject.GetComponent<CharacterMovement>();
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0,60);

            cm.Boioioing();
            SoundManager.Instance.Play(boingSound);
            animator.SetTrigger("boing");
        }
    }
}
