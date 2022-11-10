
public class InputClickerState : InputState
{
    public InputClickerState(IStationstateSwitcher stateSwitcher) : base(stateSwitcher, false)
    {
    }
    public override void Start()
    {
        InputSystem.Instance.OnTouch += OnPointerDown;
        SwipeSystem.Instance.OnSwipe += OnSwipe;
    }
    
    public override void Stop()
    {
        InputSystem.Instance.OnTouch -= OnPointerDown;
        SwipeSystem.Instance.OnSwipe -= OnSwipe;
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

    private void OnPointerDown()
    {
        EventManager.InputEvent.SendClickEvent();
    }
    private void OnSwipe(SwipeData swipeData)
    {
        if(swipeData.Direction != SwipeDirection.Up){return;}
        EventManager.InputEvent.SendOnSwipeUp();
    }
}
