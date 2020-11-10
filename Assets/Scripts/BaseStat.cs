using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }

    public BaseStatType StatType { get; set; }
    public float BaseValue { get; set; }
    public float GrowthValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float FinalValue { get; set; }

    public BaseStat(BaseStatType statType, float baseValue, float growthValue, string statName, string statDescription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.StatType = statType;
        this.BaseValue = baseValue;
        this.GrowthValue = growthValue;
        this.StatName = statName;
        this.StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public float GetCalculatedStatValue()
    {
        this.FinalValue = 0;

        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);

        FinalValue += BaseValue;

        return FinalValue;
    }
}

public enum BaseStatType
{
    BaseMovementSpeed,
    AttackDamage,
    SpellDamage,
    AttackRange,
    AttackSpeed,
    CriticalStrikeChance,
    CriticalStrikeDamage,
    Armor,
    MagicResist
}
