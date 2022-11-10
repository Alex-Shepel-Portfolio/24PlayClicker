using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : State
{
    public bool IsNeedUpdate => isNeedUpdate;
    public InputState(IStationstateSwitcher stateSwitcher, bool isNeedUpdate) : base(stateSwitcher, isNeedUpdate)
    {
    }

    public override void Start()
    {
    }

    public override void Stop()
    {
    }

    public virtual void GetOtherState(InputStateType stateType)
    {
        GetOtherState(stateType);
    }

    public override void GetOtherState(int stateIndex)
    {
    }
}

public enum InputStateType
{
    ClickerState = 1,
    SwipeMoveState = 2,
    MinZoneMoveState = 3
}
