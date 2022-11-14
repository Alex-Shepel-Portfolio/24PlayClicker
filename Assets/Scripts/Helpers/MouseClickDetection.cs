using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetection : ImprovedMonoBehaviour
{
    public Action OnMouseClick;
    public Action OnMouseDoubleClick;
    public Action OnMouseHoldAndUp;
    public Action OnMouseHold;
    public Action OnMouseDragAtObject;
    [SerializeField] private float timeToHold;
    [SerializeField] private Collider triggerCollider;

    private bool isHold;
    private bool isDouble;

    private Coroutine waitCoroutine;
    private Coroutine doubleClickWaitCoroutine;
    
    private const float minTimeForDoubleClick = 0.5f;

    public void SetActive(bool isActive)
    {
        triggerCollider.enabled = isActive;
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
        }
        if (doubleClickWaitCoroutine != null)
        {
            StopCoroutine(doubleClickWaitCoroutine);
        }
    }
    private void OnMouseDown()
    {
        OnMouseClick?.Invoke();
        if (isDouble)
        {
            OnMouseDoubleClick?.Invoke(); 
        }
        isHold = false;
        StartWaitCoroutine();
    }

    private void OnMouseUp()
    {
        StopWaitCoroutine();
        if (isHold)
        {
            OnMouseHoldAndUp?.Invoke();
        }
    }

    private void OnMouseDrag()
    {
        if(!isHold){return;}
        OnMouseDragAtObject?.Invoke();
    }

    private void StartWaitCoroutine()
    {
        waitCoroutine = StartCoroutine(WaitCoroutine());
        doubleClickWaitCoroutine = StartCoroutine(DoubleClickWait());
    }
    private void StopWaitCoroutine()
    {
       StopCoroutine(waitCoroutine);
       isHold = false;
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(timeToHold);
        OnMouseHold?.Invoke();
        isHold = true;
    }
    private IEnumerator DoubleClickWait()
    {
        isDouble = true;
        yield return new WaitForSeconds(minTimeForDoubleClick);
        isDouble = false;
    }
    
}
