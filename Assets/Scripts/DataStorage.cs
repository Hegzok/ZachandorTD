using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    [SerializeField] private MouseInfo mouseInfo;
    public static MouseInfo MouseInfo;

    [SerializeField] private Transform playerHand;
    public static Transform PlayerHand;

    [SerializeField] private Ability emptyAbility;
    public static Ability EmptyAbility;

    private void Awake()
    {
        MouseInfo = mouseInfo;
        PlayerHand = playerHand;
        EmptyAbility = emptyAbility;
    }

}
