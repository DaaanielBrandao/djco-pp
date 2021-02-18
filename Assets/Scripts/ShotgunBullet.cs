﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float speed = 50f;
    public float maxTime = 0.3f;
    private float dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = GameObject.Find("Character").GetComponent<CharacterMovement>().turned;
    }

    // Update is called once per frame
    void Update()
    {
        //por cooldwon
        transform.Translate(Vector3.right * dir * Time.deltaTime * speed);
        
        maxTime -= Time.deltaTime;
        if(maxTime <= 0){
            Destroy(gameObject);
        }
    }
    //desaparecer qnd se espetar e tal
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }
}   