using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> baseStats = new List<BaseStat>();

    public CharacterStats(List<PlayerStatObject> baseStatObjects)
    {
        foreach (var stat in baseStatObjects)
        {
            baseStats.Add(stat.ReturnBaseStat());
        }
    }

    public CharacterStats(List<EnemyStatObject> baseStatObjects)
    {
        foreach (var stat in baseStatObjects)
        {
            baseStats.Add(stat.ReturnBaseStat());
        }
    }

    public BaseStat GetStat(BaseStatType stat)
    {
        return this.baseStats.Find(x => x.StatType == stat);
    }

    public float GetStatFinalValue(BaseStatType stat)
    {
        return GetStat(stat).GetCalculatedStatValue();
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
