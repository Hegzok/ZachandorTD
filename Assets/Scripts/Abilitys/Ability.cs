using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private GameObject abilityPrefab;
    public GameObject AbilityPrefab => abilityPrefab;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] private string abilityName;
    public string AbilityName => abilityName;

    [SerializeField] private float cooldown;
    public float Cooldown => cooldown;

    [SerializeField] private int damage;
    public int Damage => damage;

    [SerializeField] protected AbilityType abilityType;
    public AbilityType AT => abilityType;

    public virtual void Use()
    {
        Debug.Log("Used default ability");
    }

}

public enum AbilityType
{
    Fire,
    Ice,
    Nature
}
