using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(float power, float defense, float attackSpeed)
    {
        stats = new List<BaseStat>()
        {
            new BaseStat(BaseStatType.AttackDamage, power, 0.3f, "Power", "Determines how hard you hit"),
            new BaseStat(BaseStatType.Armor, defense, 0.3f, "Defense", "Determines how much less damage you take"),
            new BaseStat(BaseStatType.AttackSpeed, attackSpeed, 0.3f, "Atk Spd", "Determines how fast you attack")
        };
    }

    public BaseStat GetStat(BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

}
