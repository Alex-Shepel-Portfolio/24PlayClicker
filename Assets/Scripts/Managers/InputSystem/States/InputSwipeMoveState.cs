using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwipeMoveState : InputState
{

    public InputSwipeMoveState(IStationstateSwitcher stateSwitcher) : base(stateSwitcher, true)
    {
    }

    private float lastDist = 0;
    private float touchDist = 0;
    

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

    public override void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftControl))
        {
            EventManager.InputEvent.SendOnDoubleTouch( Input.mouseScrollDelta.y);
        }
        #endif
        
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
 
            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                lastDist = Vector2.Distance(touch1.position, touch2.position);
            }
 
            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                float newDist = Vector2.Distance(touch1.position, touch2.position);
                touchDist = lastDist - newDist;
                lastDist = newDist;
                EventManager.InputEvent.SendOnDoubleTouch(touchDist);
            }
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
