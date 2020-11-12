using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "Enemy stats/BaseStat", order = 0)]
public class EnemyStatObject : ScriptableObject
{
    public BaseStat baseStat { get; set; }

    [SerializeField]
    private BaseStatType baseStatType;
    [SerializeField]
    private float baseValue;
    [SerializeField]
    private float growthValue;

    public BaseStat ReturnBaseStat()
    {
        baseStat = new BaseStat(baseStatType, baseValue, growthValue);

        return baseStat;
    }
}
