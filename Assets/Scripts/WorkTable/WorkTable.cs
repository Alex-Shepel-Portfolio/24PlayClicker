using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTable : ImprovedMonoBehaviour
{
    [SerializeField] private TimeScaleController timeScaleController;
    [SerializeField] private ComputerController computerController;
    [SerializeField] private Worker worker;
    private bool isHasWorker;

    public bool IsHasWorker
    {
        get => isHasWorker;
        set
        {
            isHasWorker = value;
            OnWorkerStateChange?.Invoke(value);
        }
    }

    public Action<bool> OnWorkerStateChange;
    
    private bool isPlayerLook;

    public bool IsPlayerLook
    {
        get => isPlayerLook;
        set
        {
            isPlayerLook = value;
            OnPlayerLook?.Invoke(value);
        }
    }

    public Action<bool> OnPlayerLook;
    
    public void Init()
    {
        timeScaleController.Init();
        computerController.Init();
        computerController.OnWorkDone += SendProgress;
        OnWorkerStateChange += SendWorkerStatus;
        OnPlayerLook += SendPlayerLookStatus;
        timeScaleController.OnTimeScaleChanged += OnStartWorkFaster;
        computerController.SetWorker(worker);
        IsHasWorker = true;
        IsPlayerLook = true;
        timeScaleController.SetParameters(worker.GetWorkerDamage());

    }

    private void SendWorkerStatus(bool isHasWorker)
    {
        computerController.IsHasWorker(isHasWorker);
    }
    private void SendPlayerLookStatus(bool isLook)
    {
        computerController.IsPlayerLook(isLook);
    }

    public void SetWorker(Worker worker)
    {
        this.worker = worker;
    }
    
    private void SendProgress()
    {
       var workerDamage = worker.GetWorkerDamage();
        EventManager.SendWorkDone(workerDamage);
        computerController.IsHasWorker(true);
    }

    private void OnStartWorkFaster(float workTime)
    {
        var writeSpeed = workTime * worker.GetWorkerWriteSpeed();
        computerController.UpdateWorkSpeed(writeSpeed);
    }
}
