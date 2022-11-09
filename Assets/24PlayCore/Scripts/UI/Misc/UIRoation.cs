using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIRoation : ImprovedMonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private void OnEnable()
    {
        transform.DOLocalRotate(transform.localEulerAngles + Vector3.forward * rotationSpeed, 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}