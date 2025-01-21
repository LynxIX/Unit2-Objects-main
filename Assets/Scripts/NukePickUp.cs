using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : PickUp
{
    protected override void PickMeUp(Player playerInTrigger)
    {
        if (playerInTrigger != null)
        {
            playerInTrigger.AddNuke();

            Destroy(gameObject);
        }
        else
        {

        }
    }
}