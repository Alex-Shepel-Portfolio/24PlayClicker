using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WorkTableUI : InteractivePanel
{
    [SerializeField] private CanvasCameraLook cameraLook;

    protected override void Start()
    {
        base.Start();
        GameC.Instance.OnLevelLoaded += InitWhenLevelLoaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameC.Instance.OnLevelLoaded -= InitWhenLevelLoaded;
    }

    private void InitWhenLevelLoaded(int _)
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        HideInstant();
        cameraLook.Init();
    }

    public override void Show()
    {
        base.Show();
        cameraLook.StartLookAtCamera();
    }

    public override void Hide()
    {
        base.Hide();
        cameraLook.StopLookAtCamera();
    }
}
