using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Semiautomatic Weapon")]
public class SemiAutoPWeapon : ProjectileWeapon
{
    public override void Reload()
    {
        
    }

    public override void Shoot(Transform weaponTip)
    {
        if(isShooting)
        {
            Bullet bulletClone;

            bulletClone = GameObject.Instantiate(
                projectilePrefab, 
                weaponTip.position, 
                weaponTip.rotation);

            bulletClone.InitializeBullet(damage);
        }

    }

    public override void StartShooting(Transform weaponTip)
    {
        isShooting = true;
        Shoot(weaponTip);
        StopShooting();
    }

    public override void StopShooting()
    {
        isShooting = false;
    }
}
