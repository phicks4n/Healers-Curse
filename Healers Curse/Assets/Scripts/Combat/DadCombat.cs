using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadCombat : MonoBehaviour
{

    [Header("INK JSON")]
    [SerializeField] private TextAsset inkJSON;

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
            currentHP = currentHP - (dmg - (int)(.35 * armor));
            damageTaken = (dmg - (int)(.35 * armor));
        }
        else if (((currentHealth / maxHealth) * 100) < .3 * maxHealth)
        {
            currentHP = currentHP - (dmg - (int)(.15 * armor));
            damageTaken = (dmg - (int)(.15 * armor));
        }

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public void BattleEnd()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
