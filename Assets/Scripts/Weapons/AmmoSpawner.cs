using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public float cooldown;    

    private bool canSpawn;
    private PlayerDetectorAll detector;
    private Collider2D[] areas;

    // Start is called before the first frame update
    private void Start()
    {
        detector = gameObject.AddComponent<PlayerDetectorAll>();
        detector.radius = 100;
        
        areas = gameObject.GetComponentsInChildren<Collider2D>();
    }

    private void OnEnable() {
        canSpawn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canSpawn)
            StartCoroutine(SpawnAmmo());        
    }

    private IEnumerator SpawnAmmo() {
        GameObject[] players = detector.nearbyPlayers;
        if (players.Length > 0) {

            // Get random ammo box
            GameObject player = players[Random.Range(0, players.Length)];
            Weapon[] playerWeapons = player.gameObject.GetComponentInChildren<WeaponSwitch>().GetWeapons();
            
            Weapon randomWeapon = playerWeapons[Random.Range(0, playerWeapons.Length)];
            
            Debug.Log(randomWeapon.GetType());
            // Get random pos and instantiate it
            Collider2D area = areas[Random.Range(0, areas.Length)];
            Bounds bounds = area.bounds;
            Vector2 randomPos = new Vector2(Random.Range(bounds.min.x, bounds.max.x),Random.Range(bounds.min.y, bounds.max.y));
            GameObject createdAmmo = GroundFinder.SpawnOnGround(randomWeapon.ammoBox, randomPos);
            createdAmmo.GetComponent<AmmoBox>().SetWeaponType(randomWeapon.GetType());
            
            canSpawn = false;
            yield return new WaitForSeconds(cooldown);
            canSpawn = true;
        }
    }

}
