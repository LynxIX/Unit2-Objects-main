using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] protected Rigidbody2D myRigidbody;
    [SerializeField] protected float movementSpeed = 10f;

    [SerializeField] private GameObject dieEffect;


    public Health healthValue;
    public Weapon currentWeapon;

    protected virtual void Start()
    {
        healthValue = new Health(health);
        healthValue.OnDied.AddListener(PlayDeadEffect);
        
    }

    public virtual void Move(Vector2 direction)
    {
        myRigidbody.AddForce(direction * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
    }

    public virtual void Look(Vector2 direction)
    {
        float angle; // = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Vector2.SignedAngle(Vector2.up, direction);
        myRigidbody.SetRotation(angle);
    }

    public virtual void PlayDeadEffect()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Interact()
    {
        //Debug.Log(GameManager.timer);
    }

    //Probably happening every frame
    public virtual void Attack() //the part where it instantiate the bullet
    {
        //Debug.Log("Punching!");
    }

    public virtual void StartAttack()
    {
        //As I press the mouse button
    }

    public virtual void StopAttack()
    {
        //When I release the mouse button
    }

}
