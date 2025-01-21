using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Charging Weapon")]
public class ChargingPWeapon : ProjectileWeapon
{
    [SerializeField] private float chargingRequiredTime;
    private float chargingTimer;

    public override void Reload()
    {
       
    }

    public override void Shoot(Transform weaponTip)
    {
        if(isShooting)
        {
            if (chargingTimer > chargingRequiredTime)
            {
                //Instantiate
                Bullet bulletClone = GameObject.Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
                bulletClone.InitializeBullet(damage);
                StopShooting();
            }
            else
            {
                //Find access to charging bar and notify
                //Send chargingTimer to a UI component that handles
                chargingTimer += Time.deltaTime;
            }
        }

    }

    public override void StartShooting(Transform weaponTip)
    {
        //You need access to a audio source and play a certain charging sound
       
        isShooting = true;
        chargingTimer = 0;
    }

    public override void StopShooting()
    {
        //Access that audio source and stop the charging sound
        isShooting = false;
        chargingTimer = 0;
    }
}
