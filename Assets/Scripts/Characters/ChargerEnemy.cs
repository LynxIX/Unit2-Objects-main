using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : Enemy
{
    [SerializeField] private float chargeSpeedMultiplier = 2f;
    [SerializeField] private float highDamage = 10f;

    protected override void Start()
    {
        base.Start();
    }

    public override void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * Time.deltaTime * movementSpeed * chargeSpeedMultiplier, ForceMode2D.Impulse);
    }

    public override void Attack()
    {
        if (attackTimer >= attackCooldown)
        {
            target.healthValue.DecreaseHealth(highDamage);
            attackTimer = 0;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }
}