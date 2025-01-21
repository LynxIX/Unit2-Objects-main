using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUp
{
    [SerializeField] private int healthPointToAdd;

    protected override void PickMeUp(Player playerInTrigger)
    {
        if (!playerInTrigger) Debug.Log("Player not found");
        playerInTrigger.healthValue.IncreaseHealth(healthPointToAdd);
        Destroy(gameObject);
    }
}
