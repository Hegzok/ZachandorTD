using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State<Enemy>
{
    public override void InitState(Enemy controller)
    {
        
    }

    public override void UpdateState(Enemy controller)
    {
        controller.StartPatrolling();
    }

    public override void DeinitState(Enemy controller)
    {
        
    }
}
