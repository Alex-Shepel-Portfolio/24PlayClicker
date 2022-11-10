using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<float> OnWorkDone = new UnityEvent<float>();

    public static void SendWorkDone(float progressValue)
    {
        OnWorkDone?.Invoke(progressValue);
    }
    
    public static class InputEvent
    {
        public static UnityEvent OnClick = new UnityEvent();
        public static UnityEvent OnSwipeUp = new UnityEvent();
        public static UnityEvent<Vector2> OnDrag = new UnityEvent<Vector2>();
        public static UnityEvent<Vector2> OnMinDrag = new UnityEvent<Vector2>();

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
    }
}



