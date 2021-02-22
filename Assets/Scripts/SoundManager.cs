using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton gamings
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }
    
    private void Awake() {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else _instance = this;
    }


    public AudioClip jumpSound; 
    public AudioClip dropSound; 
    public AudioClip dashSound; 
    public AudioClip pistolSound; 
    public AudioClip shotgunSound; 
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
        if (_instance == null || _instance != this) // helps with not breaking
            _instance = this;

        if (Input.GetKeyDown(KeyCode.P))
            playerAudio.PlayOneShot(pickleSound);
    }

    public void OnJump() {
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    public void OnDrop() {
        //playerAudio.PlayOneShot(dropSound, 1.0f);
    }

    public void OnDash() {
        playerAudio.PlayOneShot(dashSound, 1.0f);
    }

    public void OnShootPistol() {
        playerAudio.PlayOneShot(pistolSound, 1.0f);
    }
    public void OnShootShotgun() {
        playerAudio.PlayOneShot(shotgunSound, 1.0f);
    }
}
