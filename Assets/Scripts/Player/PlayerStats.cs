using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
    private float baseMovementSpeed;
    public float BaseMovementSpeed => baseMovementSpeed;

    [SerializeField] [Range(0f, 20f)]
    private float currentMovementSpeed;
    public float CurrentMovementSpeed => currentMovementSpeed;

    [SerializeField] [Range(0f, 3f)]
    private float movementSpeedBackwardsMultiplier;
    public float MovementSpeedBackwards => CurrentMovementSpeed / movementSpeedBackwardsMultiplier;

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    [SerializeField] [Range(0f, 20f)]
    private float radius;
    public float Radius => radius;
}
