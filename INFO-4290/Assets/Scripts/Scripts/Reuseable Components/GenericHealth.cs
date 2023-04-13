
using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHealth = maxHealth.RuntimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth.RuntimeValue)
        {
            currentHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.RuntimeValue;
    }

    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public virtual void InstantDeath()
    {
        currentHealth = 0;
    }
}
