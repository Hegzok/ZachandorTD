﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : Ability
{
    [SerializeField] private float speed;
    public float Speed => speed;

    // Start is called before the first frame update
    void Start()
    {
        abilityType = AbilityType.Basic;
        transform.LookAt(DataStorage.MouseInfo.ReturnMousePos(this.transform));
        Destroy(this.gameObject, 5f);

        // temporary change
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public override void Use()
    {
        Debug.Log($"Used {this.AbilityName}");
        Instantiate(this.AbilityPrefab, DataStorage.PlayerHand.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Player>() && !other.gameObject.GetComponent<Ability>())
        {
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();

            if (damagable != null)
            {
                int damage = Mathf.RoundToInt(DataStorage.Player.ReturnFinalPhysicalDamage(damagable));
                damagable.TakeDamage(damage);
                Debug.Log($"enemy took {damage} damage");

                // temporary change later
                damagePopUp.InstantiateDamageText(Color.green, damage.ToString(), this.transform);
            }

            Destroy(this.gameObject);
        }
    }
}
