using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private EnemyStats enemyStats;

    private Stats stats;
    public Stats Stats => stats;

    // Start is called before the first frame update
    void Start()
    {
        InitStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int value)
    {
        stats.CurrentHealth -= value;

        Die();
    }

    private void Die()
    {
        if (stats.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void InitStats()
    {
        stats.CurrentHealth = enemyStats.MaxHealth;
        stats.Damage = enemyStats.Damage;
        stats.MovementSpeed = enemyStats.MovementSpeed;
        stats.MaxHealth = enemyStats.MaxHealth;
    }

}
