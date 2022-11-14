using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseClickDetection : ImprovedMonoBehaviour,IPointerDownHandler
{
    public Action<Vector2> OnClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
      
        OnClicked?.Invoke(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClicked?.Invoke(eventData.position);
    }
}
