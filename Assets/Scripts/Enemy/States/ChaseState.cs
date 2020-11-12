using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<Enemy>
{
    private Player player;

    public override void InitState(Enemy controller)
    {
        player = DataStorage.Player;
        controller.MakeEnemyAwareOfPlayer(true);
        controller.ChangeStoppingDistance(1.5f);
    }

    public override void UpdateState(Enemy controller)
    {
        controller.FollowPlayer(player.transform.position);
        controller.CheckIfPlayerInRangeToAttack(player);
    }

    public override void DeinitState(Enemy controller)
    {

    }
}
