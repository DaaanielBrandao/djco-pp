using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    public AudioClip jumpSound; 
    public AudioClip dropSound; 
    public AudioClip dashSound; 
    public AudioClip pickleSound; 
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            playerAudio.PlayOneShot(pickleSound);
    }

    public void OnJump() {
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    public void OnDrop() {
        playerAudio.PlayOneShot(dropSound, 1.0f);
    }

    public void OnDash() {
        playerAudio.PlayOneShot(dashSound, 1.0f);
    }
}
