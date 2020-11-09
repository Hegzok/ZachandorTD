using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
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
    private int damage;
    public int Damage => damage;

    [SerializeField]
    private float attackRange;
    public float AttackRange => attackRange;

    [SerializeField]
    private float attackCooldown;
    public float AttackCooldown => attackCooldown;
}
