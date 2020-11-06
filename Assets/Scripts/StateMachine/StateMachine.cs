using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour
{
    protected State<T> currentState;

    public abstract void ChangeState(State<T> newState);
    public abstract void UpdateState();
}
