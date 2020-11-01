using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Ability activeAbility;
    public Ability ActiveAbility => activeAbility;

    // Start is called before the first frame update
    void Start()
    {
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
}
