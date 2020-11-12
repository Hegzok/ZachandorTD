using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    public static UnityAction<Ability> OnAbilityPickUp;
    public static UnityAction<int, Ability> OnAbilityPickUpUI;
    public static UnityAction<Ability> OnBasicAttackPickUp;
    public static UnityAction<int> OnAbilityDiscard;
    public static UnityAction<int> OnHotkeyChosen;
    public static UnityAction<Ability> OnAbilityChosen;
    public static UnityAction<Enemy> OnEnemyDeath;

    public static void CallOnEnemyDeath(Enemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }

    public static void CallOnAbilityPickUp(Ability ability)
    {
        OnAbilityPickUp?.Invoke(ability);
    }

    public static void CallOnBasicAttackPickUp(Ability ability)
    {
        OnBasicAttackPickUp?.Invoke(ability);
    }

    public static void CallOnAbilityDiscard(int index)
    {
        OnAbilityDiscard?.Invoke(index);
    }

    public static void CallOnAbilityPickUpUI(int index, Ability ability)
    {
        OnAbilityPickUpUI?.Invoke(index, ability);
    }

    public static void CallOnHotkeyChosen(int index)
    {
        OnHotkeyChosen?.Invoke(index);
    }

    public static void CallOnAbilityChosen(Ability ability)
    {
        OnAbilityChosen?.Invoke(ability);
    }
}
