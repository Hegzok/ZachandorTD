using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ability
{
    [SerializeField] private float speed;
    public float Speed => speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(DataStorage.MouseInfo.ReturnMousePos(this.transform));
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
        if (!other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);
            Destroy(this.gameObject);
        }
    }



}
