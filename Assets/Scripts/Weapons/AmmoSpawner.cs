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
        detector.radius = 50;
        
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
            GameObject ammoBox = playerWeapons[Random.Range(0, playerWeapons.Length)].ammoBox;
            
            // Get random pos
            Collider2D area = areas[Random.Range(0, areas.Length)];
            Bounds bounds = area.bounds;
            Vector2 position = new Vector2(Random.Range(bounds.min.x, bounds.max.x),Random.Range(bounds.min.y, bounds.max.y));
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D contact = Physics2D.Raycast(position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground", "Platform"));
            Physics2D.queriesHitTriggers = true;
            
            Debug.Log(position + " " + contact.centroid);

            Instantiate(ammoBox, contact.centroid, ammoBox.transform.rotation);
           
            
            canSpawn = false;
            yield return new WaitForSeconds(cooldown);
            canSpawn = true;
        }
    }

}
