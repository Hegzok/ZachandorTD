using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<Enemy>
{
    public override void InitState(Enemy controller)
    {
        controller.MakeEnemyAwareOfPlayer(true);
    }

    public override void UpdateState(Enemy controller)
    {
        controller.FollowPlayerWhenInRange(DataStorage.Player.transform.position);
    }

    public override void DeinitState(Enemy controller)
    {
        Debug.Log($"{controller.gameObject.name} stopped chasing player");
    }
}
