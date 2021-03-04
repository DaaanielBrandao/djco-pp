using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public float cooldown;
    
    private bool canSpawn;
    private PlayerDetectorAll detector; 

    // Start is called before the first frame update
    void Start()
    {
        detector = gameObject.AddComponent<PlayerDetectorAll>();
        detector.radius = 50;
        
    }

    private void OnEnable() {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            StartCoroutine(SpawnAmmo());        
    }

    IEnumerator SpawnAmmo() {
        GameObject[] players = detector.nearbyPlayers;
        if (players.Length > 0) {
            GameObject player = players[Random.Range(0, players.Length)];

            Weapon[] playerWeapons = player.gameObject.GetComponentInChildren<WeaponSwitch>().GetWeapons();

         
            GameObject ammoBox = playerWeapons[Random.Range(0, playerWeapons.Length)].ammoBox;
            Vector2 randomPos = new Vector2(Random.Range(-30, 30), Random.Range(0, 20)); // Hmm!

            Instantiate(ammoBox, randomPos, ammoBox.transform.rotation);
            
            canSpawn = false;
            yield return new WaitForSeconds(cooldown);
            canSpawn = true;
        }
    }
}
