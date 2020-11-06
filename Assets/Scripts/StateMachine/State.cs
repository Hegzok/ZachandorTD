using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public abstract void InitState(T controller);
    public abstract void UpdateState(T controller);
    public abstract void DeinitState(T controller);
}
