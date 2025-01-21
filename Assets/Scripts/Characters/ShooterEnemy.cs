using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 2f;

    public override void Attack()
    {
        if (attackTimer >= attackCooldown)
        {
            if (target != null)
            {

                Vector2 direction = (target.transform.position - firePoint.position).normalized;



                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);


                bullet.transform.up = direction;


                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = direction * bulletSpeed;



                    attackTimer = 0;
                }
                else
                {
                    attackTimer += Time.deltaTime;
                }
            }
        }
    }
}
