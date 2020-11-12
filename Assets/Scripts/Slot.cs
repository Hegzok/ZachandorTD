using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private int index;

    [SerializeField] private Image icon;
    public Image Icon => icon;

    private bool empty;
    public bool Empty => empty;

    private bool activeSlot;
    public bool ActiveSlot => activeSlot;
    

    private void Awake()
    {
        empty = true;
        icon = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.OnAbilityDiscard += SetIconToEmpty;
    }

    private void OnDisable()
    {
        EventsManager.OnAbilityDiscard -= SetIconToEmpty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIcon(Ability ability)
    {
        if (empty)
        {
            icon.sprite = ability.Icon;
            empty = false;
        }
    }

    private void SetIconToEmpty(int index)
    {
        // check consider checking if already empty icon from spells[index].

        if (!empty && this.index == index)
        {
            icon.sprite = DataStorage.EmptyAbility.Icon;
            empty = true;
        }
    }
}
