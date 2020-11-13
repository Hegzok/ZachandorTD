using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : Ability
{
    [SerializeField] 
    private float speed;
    public float Speed => speed;

    [SerializeField]
    private float slowDownPercentage;

    [SerializeField]
    private float slowDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        abilityType = AbilityType.Spell;
        transform.LookAt(DataStorage.MouseInfo.ReturnMousePos(this.transform));
        Destroy(this.gameObject, 5f);
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
                damagable.TakeDamage(Mathf.RoundToInt(Damage));
                damagable.SlowDownMovementSpeed(slowDownPercentage, slowDownTimer);

                // temporary will change later
                if (other.gameObject.GetComponent<Enemy>())
                {
                    other.gameObject.GetComponent<Enemy>().ChangeBodyColorCall(Color.blue, slowDownTimer);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
