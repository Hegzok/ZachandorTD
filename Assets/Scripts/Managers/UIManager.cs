using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InventorySpells inventorySpells;
    [SerializeField] private Slot[] spellsBar;

    [SerializeField]
    private Slider healthBarSlider;
    [SerializeField]
    private Text healthBarText;

    private Slot lastActiveSlot;

    // Start is called before the first frame update
    void Start()
    {
        HandleEvents(true);
        SetMaxHealth(DataStorage.Player.Stats.MaxHealth);
        SetAllIcons();
    }

    private void OnDisable()
    {
        HandleEvents(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(DataStorage.Player.Stats.CurrentHealth);
    }

    public void SetMaxHealth(int value)
    {
        healthBarSlider.maxValue = value;
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    public void SetHealth(int value)
    {
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    private void HandleEvents(bool switcher)
    {
        if (switcher)
        {
            EventsManager.OnAbilityPickUpUI += SetIcon;
            EventsManager.OnHotkeyChosen += SetSlotActiveImage;
        }
        else
        {
            EventsManager.OnAbilityPickUpUI -= SetIcon;
            EventsManager.OnHotkeyChosen -= SetSlotActiveImage;
        }
    }

    private void SetAllIcons()
    {
        for (int i = 0; i < inventorySpells.Spells.Count; i++)
        {
            if (inventorySpells.Spells[i] != DataStorage.EmptyAbility)
            spellsBar[i].SetIcon(inventorySpells.Spells[i]);
        }
    }

    private void SetIcon(int index, Ability ability)
    {
        spellsBar[index].SetIcon(ability);
    }

    public void AbilityDiscard(int index)
    {
        EventsManager.CallOnAbilityDiscard(index);
    }

    private void SetSlotActiveImage(int index)
    {
        lastActiveSlot?.SetActiveImage(false);

        spellsBar[index].SetActiveImage(true);

        lastActiveSlot = spellsBar[index];    
    }
}
