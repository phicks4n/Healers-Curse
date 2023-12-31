using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyUnitLevel;

    public float maxHP;
    public float currentHP;
    public int armor;

    public float damage;
    public float damageTaken;

    // parameters are character dmg, enemy armor, characterCurrentHealth, characterMaxHealth
    public bool TakeDamage(float dmg, int armor, float currentHealth, float maxHealth)
    {
        if (((currentHealth / maxHealth) * 100) >= .3 * maxHealth)
        {
            if (dmg < (int) (.3 * armor))
            {
                currentHP = currentHP - 1;
                damageTaken = 1;
            }
            else
            {
                currentHP = currentHP - (dmg - (int)(.15 * armor));
                damageTaken = (dmg - (int)(.15 * armor));
            }
        }
        else if (((currentHealth / maxHealth) * 100) < .3 * maxHealth)
        {
            if (dmg < (int) (.15 * armor))
            {
                currentHP = currentHP - 1;
                damageTaken = 1;
            }
            else
            {
                currentHP = currentHP - (dmg - (int)(.05 * armor));
                damageTaken = (dmg - (int)(.05 * armor));
            }
        }

        if (currentHP <= 0)
            return true;
        else
            return false;

    }
}
