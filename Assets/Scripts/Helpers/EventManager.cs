using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<float> OnWorkDone = new UnityEvent<float>();
    public static UnityEvent<WorkTable, bool> OnPlayModeChange = new UnityEvent<WorkTable, bool> ();

    public static void SendWorkDone(float progressValue)
    {
        OnWorkDone?.Invoke(progressValue);
    }
    public static void SendOnPlayModeChange(WorkTable currentWorkTable, bool isActivePersonMode)
    {
        OnPlayModeChange?.Invoke(currentWorkTable,isActivePersonMode );
    }
    
    public static class InputEvent
    {
        public static UnityEvent OnClick = new UnityEvent();
        public static UnityEvent OnSwipeUp = new UnityEvent();
        public static UnityEvent<Vector2> OnDrag = new UnityEvent<Vector2>();
        public static UnityEvent<Vector2> OnMinDrag = new UnityEvent<Vector2>();
        public static UnityEvent<float> OnDoubleTouch = new UnityEvent<float>();

        public static void SendClickEvent()
        {
            OnClick?.Invoke();
        }
        public static void SendDragEvent(Vector2 dragDirection)
        {
            OnDrag?.Invoke(dragDirection);
        }
        public static void SendMinDragEvent(Vector2 dragDirection)
        {
            OnMinDrag?.Invoke(dragDirection);
        }

        public static void SendOnSwipeUp()
        {
            OnSwipeUp?.Invoke();
        }

        public static void SendOnDoubleTouch(float distanceWithTouch)
        {
            OnDoubleTouch?.Invoke(distanceWithTouch);
        }
    }
}



