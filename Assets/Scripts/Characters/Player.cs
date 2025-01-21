using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class Player : Character
{
    [SerializeField] private Transform playerWeaponTip;
    [SerializeField] private TMPro.TextMeshProUGUI nukeCounterText;

    private int nukeCount = 0;
   
    private Coroutine fireRateBuffCoroutine;

    private float originalFireRate;


    public static UnityEvent OnNukeActivated = new UnityEvent();

    private void Awake()

    {
        UpdateNukeCounterUI();

        if (currentWeapon != null)
        {
            originalFireRate = currentWeapon.fireRate;

        }


    }

    public void AddNuke()
    {
        nukeCount++;
        UpdateNukeCounterUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && nukeCount > 0)
        {
            ActivateNuke();
        }
    }

    private void ActivateNuke()
    {
        nukeCount--;
        UpdateNukeCounterUI();

        OnNukeActivated.Invoke();

    }
    private void UpdateNukeCounterUI()
    {
        nukeCounterText.text = $"{nukeCount}";
    }

    public override void StartAttack()
    {
        base.StartAttack();
        currentWeapon.StartShooting(playerWeaponTip);
    }

    public override void Attack()
    {
        currentWeapon.Shoot(playerWeaponTip);
    }

    public override void StopAttack()
    {
        base.StopAttack();
        currentWeapon.StopShooting();
    }
    public void ActivateFireRateBuff(float duration, float newFireRate)
    {
        if (fireRateBuffCoroutine != null)
        {
            StopCoroutine(fireRateBuffCoroutine);
        }

        fireRateBuffCoroutine = StartCoroutine(HandleFireRateBuff(duration, newFireRate));
    }

    private IEnumerator HandleFireRateBuff(float duration, float newFireRate)
    {
        if (currentWeapon != null)
        {

            originalFireRate = currentWeapon.fireRate;
            currentWeapon.fireRate = newFireRate;

            //Debug.Log("Fire rate buff activated!");
        }

        yield return new WaitForSeconds(duration);

        if (currentWeapon != null)
        {

            currentWeapon.fireRate = originalFireRate;
            //Debug.Log("Fire rate buff deactivated!");
        }
    }

}
