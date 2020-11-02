using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        Debug.Log($"Used {this.AbilityName}");

        GameObject tempSpell = Instantiate(this.AbilityPrefab, DataStorage.Player.transform.position, Quaternion.identity);

        tempSpell.transform.SetParent(DataStorage.Player.transform);

        DataStorage.Player.Heal(Damage);
    }
}
