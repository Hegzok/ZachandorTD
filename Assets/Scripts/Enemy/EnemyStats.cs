using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    private float baseMovementSpeed;
    public float BaseMovementSpeed => baseMovementSpeed;

    [SerializeField]
    [Range(0f, 20f)]
    private float currentMovementSpeed;
    public float CurrentMovementSpeed => currentMovementSpeed;

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField]
    private int attackDamage;
    public int AttackDamage => attackDamage;

    [SerializeField]
    private int spellDamage;
    public int SpellDamage => spellDamage;

    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField]
    private int armor;
    public int Armor => armor;

    [SerializeField]
    private int magicResist;
    public int MagicResist => magicResist;
}
