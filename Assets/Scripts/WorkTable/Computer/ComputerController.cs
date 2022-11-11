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
        textWriter.StartWriteText(worker.GetWorkerWriteSpeed(),isLastWork);
    }
    
    private void StopWork()
    {
        textWriter.StopWriteText();
    }
}
