using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float distanceToStop = 2f;
    [SerializeField] protected float attackCooldown = 3f;

    protected float attackTimer;
    protected Player target;

    [Header("Pickup Settings")]
    [SerializeField] protected GameObject[] pickupPrefabs; 
    [SerializeField] protected float dropChance = 0.5f;

    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;


    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>();
        Player.OnNukeActivated.AddListener(OnNuked);

    }

    private void OnDestroy()
    {
        Player.OnNukeActivated.RemoveListener(OnNuked);
    }

    private void OnNuked()
    {
        PlayDeadEffect();
    }


    private void Update()
    {
        if (!target) return;

        Vector2 destination = target.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = destination - currentPosition;

        if(Vector2.Distance(destination, currentPosition) > distanceToStop)
        {
            Move(direction.normalized);          
        }
        else
        {
            Attack();
        }

        Look(direction.normalized);
    }

    public override void Attack()
    {
        base.Attack();

        if(attackTimer >= attackCooldown)
        {
            target.healthValue.DecreaseHealth(1);
            //currentWeapon.StartShooting(weaponTip);
            attackTimer = 0;
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
        
    }

    public override void PlayDeadEffect()
    {
        DropRandomPickup();
        //Another Solution without GameManager
        ScoreManager.Instance.IncreaseScoreByValue(scoreValue);
        GameManager.instance.RemoveEnemyFromList(this);
        base.PlayDeadEffect();
    }

    private void DropRandomPickup()
    {
        
        if (pickupPrefabs.Length > 0 && Random.value <= dropChance)
        {
            
            GameObject pickupToSpawn = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];

            
            Instantiate(pickupToSpawn, transform.position, Quaternion.identity);
        }
    }
}
