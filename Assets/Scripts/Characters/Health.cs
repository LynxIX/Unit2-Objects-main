using UnityEngine;
using UnityEngine.Events;
public class Health
{
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnDied;
    private float healthValue;
    private Character myCharacter;

    public void DecreaseHealth(float damageParameter)
    {
        healthValue -= damageParameter;
       

        OnHealthChanged.Invoke(healthValue);

        //UPDATE THE UI
        //CHECK IF IS DEAD
        if(IsDead())
        {
            OnDied.Invoke();

        }
    }

    public void IncreaseHealth(float increaseParameter)
    {
        healthValue += increaseParameter;

        OnHealthChanged.Invoke(healthValue);

    }


    public bool IsDead()
    {
        return healthValue <= 0;
    }

    public float GetHealthValue()
    {
        return healthValue;
    }

    public Health()
    {
        healthValue = 100;
        OnDied = new UnityEvent();
        OnHealthChanged = new UnityEvent<float>();
    }

    public Health(float initialHealth)
    {
        healthValue = initialHealth;
        OnDied = new UnityEvent();
        OnHealthChanged = new UnityEvent<float>();
    }




}