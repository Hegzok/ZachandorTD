using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] 
    protected GameObject abilityPrefab;
    public GameObject AbilityPrefab => abilityPrefab;

    [SerializeField] 
    protected Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] 
    protected string abilityName;
    public string AbilityName => abilityName;

    [SerializeField] 
    protected float cooldown;
    public float Cooldown => cooldown;

    [SerializeField] 
    protected float damage;
    public float Damage => damage;

    [SerializeField] 
    protected AbilityType abilityType;
    public AbilityType AT => abilityType;

    [SerializeField] 
    protected DamagePopUp damagePopUp;

    public virtual void Use()
    {
        Debug.Log("Used default ability");
    }

}

public enum AbilityType
{
    Basic,
    Spell
}
