using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    [SerializeField] 
    private MouseInfo mouseInfo;
    public static MouseInfo MouseInfo;

    [SerializeField] 
    private Transform playerHand;
    public static Transform PlayerHand;

    [SerializeField] 
    private Ability emptyAbility;
    public static Ability EmptyAbility;

    [SerializeField] 
    private Player player;
    public static Player Player;

    [SerializeField] 
    private Transform allPatrolPointsParent;
    public static Transform AllPatrolPointsParent;

    [SerializeField]
    private UIManager uiManager;
    public static UIManager UIManager;

    private void Awake()
    {
        MouseInfo = mouseInfo;
        PlayerHand = playerHand;
        EmptyAbility = emptyAbility;
        Player = player;
        AllPatrolPointsParent = allPatrolPointsParent;
        UIManager = uiManager;
    }

}
