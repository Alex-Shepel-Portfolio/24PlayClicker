using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwipeMoveState : InputState
{

    public InputSwipeMoveState(IStationstateSwitcher stateSwitcher) : base(stateSwitcher, false)
    {
    }

    public override void Start()
    {
        InputSystem.Instance.OnDragAction += OnDragAction;
    }

    public override void GetOtherState(InputStateType stateType)
    {
        switch (stateType)
        {
            case InputStateType.ClickerState:
                stateSwitcher.SwitchState<InputClickerState>();
                break;
            case InputStateType.SwipeMoveState:
                stateSwitcher.SwitchState<InputSwipeMoveState>();
                break;
            case InputStateType.MinZoneMoveState:
                stateSwitcher.SwitchState<InputMinZoneMoveState>();
                break;
        }
    }

    public override void Stop()
    {
        InputSystem.Instance.OnDragAction -= OnDragAction;
    }
    

    private void OnDragAction(Vector2 dragDirection)
    {
        EventManager.InputEvent.SendDragEvent(dragDirection);
    }
}
