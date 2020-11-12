using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Slot[] spellsBar;
    [SerializeField] private Slot basicAttackSlot;

    [SerializeField]
    private Slider healthBarSlider;
    [SerializeField]
    private Text healthBarText;

    private Slot lastActiveSlot;

    // Start is called before the first frame update
    void Start()
    {
        HandleEvents(true);
        SetMaxHealth(DataStorage.Player.BaseStats.GetStat(BaseStatType.MaxHealth).GetCalculatedStatValue());
        SetAllIcons();
    }

    private void OnDisable()
    {
        HandleEvents(false);
    }

    private void HandleEvents(bool switcher)
    {
        if (switcher)
        {
            EventsManager.OnAbilityPickUpUI += SetIcon;
            EventsManager.OnBasicAttackPickUp += SetIcon;
        }
        else
        {
            EventsManager.OnAbilityPickUpUI -= SetIcon;
            EventsManager.OnBasicAttackPickUp -= SetIcon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(DataStorage.Player.DynamicStats.CurrentHealth);
    }

    public void SetMaxHealth(float value)
    {
        healthBarSlider.maxValue = value;
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    public void SetHealth(float value)
    {
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    private void SetAllIcons()
    {
        for (int i = 0; i < inventory.Spells.Count; i++)
        {
            if (inventory.Spells[i] != DataStorage.EmptyAbility)
            spellsBar[i].SetIcon(inventory.Spells[i]);
        }

        SetIcon(inventory.BasicAttack);
    }

    private void SetIcon(int index, Ability ability)
    {
        spellsBar[index].SetIcon(ability);
    }

    private void SetIcon(Ability ability)
    {
        basicAttackSlot.SetIcon(ability);
    }

    public void AbilityDiscard(int index)
    {
        EventsManager.CallOnAbilityDiscard(index);
    }
}
