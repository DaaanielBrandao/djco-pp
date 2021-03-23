using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LGSBullet : Bullet
{
    public float spreadAngle = 40;
    public int numBullets = 6;

    public GameObject bullets;
    // Start is called before the first frame update

    protected override IEnumerator  DestroyAfterLifetime() {
        yield return new WaitForSeconds(maxTime);
        for(int i = 0; i < numBullets; i++){
            float angle = Random.Range(-spreadAngle/2,spreadAngle/2);
            Bullet.SpawnBullet(bullets, shooter, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        }
        Destroy(gameObject);
    }
}
