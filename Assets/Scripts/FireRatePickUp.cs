using UnityEngine;

public class FireRatePickup : PickUp
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private float fireRateMultiplier = 2f;

    protected override void PickMeUp(Player playerInTrigger)
    {
        if (playerInTrigger == null || playerInTrigger.currentWeapon == null) return;


        float newFireRate = playerInTrigger.currentWeapon.fireRate * fireRateMultiplier;


        playerInTrigger.ActivateFireRateBuff(duration, newFireRate);


        UIManager.Instance.StartFireRateSlider(duration);

        Destroy(gameObject);
    }
}
