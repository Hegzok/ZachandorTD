using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private Ability activeAbility;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Stats stats;
    public Stats Stats => stats;

    // Start is called before the first frame update
    void Start()
    {
        InitPlayer();
        EventsManager.OnAbilityChosen += GetActiveAbility;
    }

    private void OnDisable()
    {
        EventsManager.OnAbilityChosen -= GetActiveAbility;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseAbility();
        }
    }

    private void UseAbility()
    {
        activeAbility.Use();
    }

    private void GetActiveAbility(Ability ability)
    {
        activeAbility = ability;
    }

    private void InitPlayer()
    {
        stats.CurrentHealth = playerStats.MaxHealth;
        stats.MovementSpeed = playerStats.MovementSpeed;
        stats.MovementSpeedBackwards = playerStats.MovementSpeedBackwards;
        stats.MaxHealth = playerStats.MaxHealth;
    }

    public void TakeDamage(int value)
    {
        stats.CurrentHealth -= value;

        Die();
    }

    public void Heal(int value)
    {
        if (stats.CurrentHealth <= stats.MaxHealth)
        {
            stats.CurrentHealth += value;
        }
        if (stats.CurrentHealth > stats.MaxHealth)
        {
            stats.CurrentHealth = stats.MaxHealth;
        }
    }

    private void Die()
    {
        if (stats.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
