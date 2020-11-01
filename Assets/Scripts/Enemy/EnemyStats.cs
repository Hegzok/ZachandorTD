using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    [Range(0f, 20f)]
    private float movementSpeed;
    public float MovementSpeed => movementSpeed;

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField]
    private int damage;
    public int Damage => damage;
}
