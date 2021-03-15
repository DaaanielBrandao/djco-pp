using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 
    private PlayerHP playerHp;
    private CharacterMovement movement;
    private WeaponSwitch weaponSwitch;

    private bool enableControls = true;
   // private enum PlayerState { Ready, Paused, Dead }
  //  private PlayerState state = PlayerState.Ready;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHp = GetComponent<PlayerHP>();
        movement = GetComponent<CharacterMovement>();
        weaponSwitch = GetComponentInChildren<WeaponSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (playerHp.isDead)
          state = PlayerState.Dead;

        bool isAlive = state != PlayerState.Dead;
        bool enableControls = state == PlayerState.Ready;
        
        renderer.enabled = isAlive;
        weaponSwitch.gameObject.SetActive(isAlive);
        playerCollider.enabled = isAlive;
        playerRb.isKinematic = !isAlive;
        */
        
        movement.enabled = enableControls;
        weaponSwitch.enabled = enableControls;
    }

    public void OnPause()
    {
        enableControls = false;
        //state = PlayerState.Paused;
    }

    public void OnResume()
    {
        enableControls = true;
        //state = PlayerState.Ready;
    }
}
