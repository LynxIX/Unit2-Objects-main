using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private float damage = 10f;



    protected override void Start()
    {
        base.Start();
        if (laser == null)
        {
            Debug.LogError("LineRenderer wrong");
        }
    }

    private void Update()
    {
        if (target == null) return;

        Vector2 destination = target.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = destination - currentPosition;


        if (Vector2.Distance(destination, currentPosition) > distanceToStop)
        {
            Move(direction.normalized);
            laser.enabled = false;
        }
        else
        {
            Attack();
        }

        Look(direction.normalized);
    }

    public override void Attack()
    {


        if (attackTimer >= attackCooldown)
        {

            laser.enabled = true;


            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, target.transform.position);


            RaycastHit2D hit = Physics2D.Linecast(transform.position, target.transform.position);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Player player = hit.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.healthValue.DecreaseHealth(damage);
                }
            }

            attackTimer = 0f;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }
}