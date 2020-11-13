using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator
{
    public float CalculatePlayerPhysicalDamage(Ability ability, float playerAttackDamage, float playerCriticalStrikeChance, float playerCriticalStrikeDamage)
    {
        float finalValue = 0f;

        float playerDamage = Random.Range(playerAttackDamage * 0.78f, playerAttackDamage * 1.22f);
        float spellDamage = Random.Range(ability.Damage * 0.78f, ability.Damage * 1.22f);

        finalValue = playerDamage + spellDamage;

        if (CanCrit(playerCriticalStrikeChance))
        {
            finalValue *= playerCriticalStrikeDamage;
        }

        return finalValue;
    }

    public float CalculatePlayerMagicalDamage(Ability ability, float playerSpellDamage, float playerCriticalStrikeChance, float playerCriticalStrikeDamage)
    {
        float finalValue = 0f;

        float playerDamage = Random.Range(playerSpellDamage * 0.68f, playerSpellDamage * 1.32f);
        float spellDamage = Random.Range(ability.Damage * 0.68f, ability.Damage * 1.32f);

        finalValue = playerDamage + spellDamage;

        if (CanCrit(playerCriticalStrikeChance))
        {
            finalValue *= playerCriticalStrikeDamage;
        }

        return finalValue;
    }

    public float CalculateEnemyPhysicalDamage(float attackDamage)
    {
        float finalValue = 0f;

        finalValue = Random.Range(attackDamage * 0.78f, attackDamage * 1.22f);

        return finalValue;
    }

    public float CalculateEnemyMagicalDamage(float spellDamage)
    {
        float finalValue = 0f;

        finalValue = Random.Range(spellDamage * 0.68f, spellDamage * 1.32f);

        return finalValue;
    }

    public float CalculateDamageNegated(float damage, float protection)
    {
        float finalValue = 0f;

        finalValue = damage - protection;

        if (finalValue <= 0)
        {
            finalValue = 0f;
        }

        return finalValue;
    }

    private bool CanCrit(float criticalStrikeChance)
    {
        int percentage = Mathf.RoundToInt(Random.Range(0f, 100f));

        if (percentage <= criticalStrikeChance)
        {
            Debug.Log($"Chance for crit was: {percentage} where critical strike chance were {criticalStrikeChance} and it returned true");
            return true;
        }
        else
        {
            Debug.Log($"Chance for cir was: {percentage} where critical strike chance were {criticalStrikeChance} and it returned false");
            return false;
        }
    }
}
