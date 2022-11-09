using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class UIElementAppearData
{
    public Transform elementTransform;
    public float startValue = 0f;
    public float endValue = 1f;
    public float appearElementTime = .3f;
    public float appearElementDelay = .2f;
    public Ease appearElementEase = Ease.OutBounce;
}