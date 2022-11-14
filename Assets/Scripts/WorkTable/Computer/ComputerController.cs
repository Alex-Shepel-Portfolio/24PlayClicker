using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ComputerController : ImprovedMonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private Transform computerHolder;
    [SerializeField] private float shakeStrenges;
    private Worker worker;
    private bool isPlayerLook;
    public Action OnWorkDone;

    public void Init()
    {
        textWriter.OnComplete += SendWorkDone;
    }
    public void UpdateWorkSpeed(float workTime)
    {
        textWriter.UpdateWriteTime(workTime);
    }

    private void SendWorkDone()
    {
        OnWorkDone?.Invoke();
    }
    public void IsPlayerLook(bool isLook)
    {
        var oldValue = isPlayerLook;
        isPlayerLook = isLook;
        if (!oldValue.Equals(isLook))
        {
            StopWork();
            StartDefaultWork();
        }
    }
    public void SetRate(float totalMin, float totalMax,float min, float max,float currentValue)
    {
        var valueToSet = Mathf.Lerp(totalMin, totalMax,
            Mathf.InverseLerp(min, max, currentValue));
        textWriter.SetRate(min,max,currentValue);
        if (currentValue < min)
        {
            computerHolder.DOKill();
            computerHolder.DOLocalRotateQuaternion(Quaternion.identity, 0.5f);
            return;
        }
        computerHolder.DOShakeRotation(0.3f, shakeStrenges, 2, valueToSet, false);
    }

    public void SetWorker(Worker worker)
    {
        this.worker = worker;
        IsHasWorker(worker!=null);
    }

    public void IsHasWorker(bool isHasWorker)
    {
        if (isHasWorker)
        {
            StartDefaultWork();
            return;
        }

        StopWork();
    }

    public void StartDefaultWork()
    {
        StartWork(false);
    }
    public void StartLastWork()
    {
        StartWork(true);
    }
    public void StopWorks()
    {
        StopWork();
    }
    
    private void StartWork(bool isLastWork)
    {
        textWriter.StartWriteText(worker.GetWorkerWriteSpeed(),isLastWork, isPlayerLook);
    }
    
    private void StopWork()
    {
        textWriter.StopWriteText();
    }


}
