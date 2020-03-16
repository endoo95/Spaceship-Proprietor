using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageHandler : MonoBehaviour, IShip
{
    [SerializeField]
    private ShipScriptableObject shipScriptableObject;

    public float shipHull;
    public float shipArmor;
    public int shipShield;

    private void Awake()
    {
        if (shipScriptableObject == null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        shipArmor = shipScriptableObject.maxArmor;
        shipShield = shipScriptableObject.maxShield;
        shipHull = shipScriptableObject.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float damageLeft = damage;
        if (shipShield > 0)
        {
            ModifyShield((int)(damage));
            return;
        }
        else
        {
            if (shipArmor > 0)
            {
                damageLeft -= shipArmor;
                ModifyArmor(damage);
                if (damageLeft <= 0)
                {
                    return;
                }
                damage = damageLeft;
            }
            ModifyHealth(damage);
        }
        if (shipHull <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int ModifyShield(int damage)
    {
        shipShield -= 1;
        return shipShield;
    }

    public float ModifyArmor(float damage)
    {
        shipArmor -= damage;
        return shipArmor;
    }

    public float ModifyHealth(float damage)
    {
        shipHull -= damage;
        return shipHull;
    }
}
