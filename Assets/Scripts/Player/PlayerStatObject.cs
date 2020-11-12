using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Player stats/BaseStat", order = 0)]
public class PlayerStatObject : ScriptableObject
{
    public BaseStat baseStat { get; set; }

    [SerializeField]
    private BaseStatType baseStatType;
    [SerializeField]
    private float baseValue;
    [SerializeField]
    private float growthValue;
    [SerializeField]
    private string statName;
    [SerializeField] [TextArea]
    private string statDescription;

    public BaseStat ReturnBaseStat()
    {
        baseStat = new BaseStat(baseStatType, baseValue, growthValue, statName, statDescription);

        return baseStat;
    }
}
