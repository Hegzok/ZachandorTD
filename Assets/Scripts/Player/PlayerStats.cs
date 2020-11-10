using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    private float baseMovementSpeed;
    public float BaseMovementSpeed => baseMovementSpeed;

    [SerializeField] [Range(0f, 20f)]
    private float currentMovementSpeed;
    public float CurrentMovementSpeed => currentMovementSpeed;

    [SerializeField] [Range(0f, 3f)]
    private float movementSpeedBackwardsMultiplier;
    public float MovementSpeedBackwards => CurrentMovementSpeed / movementSpeedBackwardsMultiplier;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField] [Range(0f, 20f)]
    private float visibilityRadius;
    public float VisibilityRadius => visibilityRadius;

    [SerializeField]
    private float criticalStrikeChance;
    public float CriticalStrikeChance => criticalStrikeChance;

    [SerializeField]
    private float criticalStrikeDamage;
    public float CriticalStrikeDamage => criticalStrikeDamage;

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField]
    private float attackDamage;
    public float AttackDamage => attackDamage;

    [SerializeField]
    private float spellDamage;
    public float SpellDamage => spellDamage;

    [SerializeField]
    private float armor;
    public float Armor => armor;

    [SerializeField]
    private float magicResist;
    public float MagicResist => magicResist;
}
