using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<Enemy>
{
    private Player player;

    public override void InitState(Enemy controller)
    {
        player = DataStorage.Player;
    }

    public override void UpdateState(Enemy controller)
    {
        controller.PerformAttack(player);
        controller.CheckIfPlayerInRangeToAttack(player);
    }

    public override void DeinitState(Enemy controller)
    {

    }
}
