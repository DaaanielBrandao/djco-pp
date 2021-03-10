using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : Spawner
{
    private Collider2D[] areas;
    private WeaponSwitch[] players;

    // Start is called before the first frame update
    private void Start() {
        areas = GetComponents<Collider2D>();
    }
    
    public override void OnWave(int waveNumber)
    {
        cooldown = 10000;
        maxAlive = 1;

        players = FindObjectsOfType<WeaponSwitch>();
        
        Debug.Log("Starting Ammo wave: " + maxAlive);
        
        ResetCooldown();
    }

    protected override List<GameObject> DoSpawn() {
        // Get random ammo box
        Weapon randomWeapon = players.GetRandom().GetWeapons().GetRandom();
            
        // Get random pos and instantiate it
        GameObject createdAmmo = GroundFinder.SpawnOnGround(randomWeapon.ammoBox, areas.GetValidSpawnPosition());
        createdAmmo.GetComponent<AmmoBox>().SetWeaponName(randomWeapon.name);
            
        return new List<GameObject> { createdAmmo };
    }


}
 