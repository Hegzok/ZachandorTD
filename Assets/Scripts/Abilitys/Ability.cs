using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private GameObject abilityPrefab;
    public GameObject AbilityPrefab => abilityPrefab;

    [SerializeField] protected Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] protected string abilityName;
    public string AbilityName => abilityName;

    [SerializeField] protected float cooldown;
    public float Cooldown => cooldown;

    [SerializeField] protected int damage;
    public int Damage => damage;

    public virtual void Use()
    {
        Debug.Log("Used default ability");
    }
}

enum AbilityType
{
    Fire,
    Ice,
    Nature
}
