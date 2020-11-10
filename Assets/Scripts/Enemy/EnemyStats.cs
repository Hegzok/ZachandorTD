using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    [SerializeField]
    private int currentExperience;
    public int CurrentExperience => currentExperience;

    private float baseMovementSpeed;
    public float BaseMovementSpeed => baseMovementSpeed;

    [SerializeField]
    [Range(0f, 20f)]
    private float currentMovementSpeed;
    public float CurrentMovementSpeed => currentMovementSpeed;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float attackDamage;
    public float AttackDamage => attackDamage;

    [SerializeField]
    private float spellDamage;
    public float SpellDamage => spellDamage;

    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField]
    private float armor;
    public float Armor => armor;

    [SerializeField]
    private float magicResist;
    public float MagicResist => magicResist;
}
