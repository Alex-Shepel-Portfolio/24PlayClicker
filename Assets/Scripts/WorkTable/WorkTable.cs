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
        SendNextStepToComputer();
    }

    private void SendNextStepToComputer()
    {
        if (SceneController.Instance.IsPreLastStage)
        {
            computerController.StartLastWork();
            return;
        }
        computerController.StartDefaultWork();
    }

    private void OnStartWorkFaster(float workTime)
    {
        var writeSpeed = workTime * worker.GetWorkerWriteSpeed();
        worker.ChangeAnimationSpeed(writeSpeed);
        computerController.UpdateWorkSpeed(writeSpeed);
    }
}
