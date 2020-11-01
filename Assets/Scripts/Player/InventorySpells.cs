using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpells : MonoBehaviour
{
    [SerializeField] private List<Ability> spells = new List<Ability>();
    public List<Ability> Spells => spells;

    [SerializeField] private Slot[] spellsBarUI;

    [SerializeField] private Ability[] testAbilitys;
    [SerializeField] private Ability testAbility;

    [SerializeField] private int maxSpellsNumber;


    // Start is called before the first frame update
    void Start()
    {
        HandleEvents(true);
    }

    private void OnDisable()
    {
        HandleEvents(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Testing
        if (Input.GetMouseButtonDown(1))
        {
            testAbility = testAbilitys[Random.Range(0, testAbilitys.Length)];
            EventsManager.CallOnAbilityPickUp(testAbility);
        }
    }

    private void HandleEvents(bool switcher)
    {
        if (switcher)
        {
            EventsManager.OnAbilityPickUp += AddSpell;
            EventsManager.OnAbilityDiscard += RemoveSpell;
            EventsManager.OnHotkeyChosen += SetActiveAbility;
        }
        else
        {
            EventsManager.OnAbilityPickUp -= AddSpell;
            EventsManager.OnAbilityDiscard -= RemoveSpell;
            EventsManager.OnHotkeyChosen -= SetActiveAbility;
        }
    }

    public void RemoveSpell(Ability ability)
    {
        Ability tempSpell = spells.Find(s => s.AbilityName == ability.AbilityName);
        spells.Remove(tempSpell);

        Debug.Log($"Removed {tempSpell.AbilityName}");
    }

    public void RemoveSpell(int index)
    {
        if (spells[index] != DataStorage.EmptyAbility)
        {
            Debug.Log($"Removed {spells[index].AbilityName}");
            spells[index] = DataStorage.EmptyAbility;
        }
    }

    public void AddSpell(Ability ability)
    {
        if (spells.Count >= maxSpellsNumber && !CheckForEmptySlot())
        {
            Debug.Log("Cant add more spells"); 
        }
        else if (CheckForEmptySlot())
        {
            int firstFindIndex = spells.FindIndex(s => s.AbilityName == DataStorage.EmptyAbility.AbilityName);
            spells[firstFindIndex] = ability;
            EventsManager.CallOnAbilityPickUpUI(firstFindIndex, ability);
            Debug.Log($"Added {spells[firstFindIndex]}, at the index of {firstFindIndex}");
        }
        else if (spells.Count < maxSpellsNumber && !CheckForEmptySlot())
        {
            spells.Add(ability);
            EventsManager.CallOnAbilityPickUpUI(spells.Count - 1, ability);
            Debug.Log($"Added {ability.AbilityName}");
        }
    }

    private bool CheckForEmptySlot()
    {
        Ability tempAbility = spells.Find(s => s == DataStorage.EmptyAbility);

        if (tempAbility != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Ability ReturnSpell(Ability ability)
    {
        Ability tempSpell = spells.Find(s => ability);
        Debug.Log(tempSpell.AbilityName);
        return tempSpell; 
    }

    public Ability ReturnSpell(int index)
    {
        return spells[index];
    }

    private void SetActiveAbility(int index)
    {
        EventsManager.CallOnAbilityChosen(spells[index]);
    }

}
