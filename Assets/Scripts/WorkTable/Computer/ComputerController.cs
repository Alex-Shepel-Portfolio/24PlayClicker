using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : ImprovedMonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
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
        isPlayerLook = isLook;
    }

    public void SetWorker(Worker worker)
    {
        this.worker = worker;
    }

    public void IsHasWorker(bool isHasWorker)
    {
        if (isHasWorker)
        {
            StartWork();
            return;
        }

        StopWork();
    }
    
    private void StartWork()
    {
        textWriter.StartWriteText(worker.GetWorkerWriteSpeed());
    }
    
    private void StopWork()
    {
        textWriter.StopWriteText();
    }
}
