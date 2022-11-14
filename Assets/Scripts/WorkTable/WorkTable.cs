using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class WorkTable : ImprovedMonoBehaviour
{
    [SerializeField] private MouseClickDetection mouseClickDetection;
    [SerializeField] private MMFeedbacks onClickFeedbacks;
    [SerializeField] private WorkTableUI workTableUI;
    [Space(5)]
    [SerializeField] private TimeScaleController timeScaleController;
    [SerializeField] private ComputerController computerController;
    [SerializeField] private Worker worker;
    [Space(5)] [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform workerSeatPosition;
    [SerializeField] private float workerSeatDuration;
    public Transform CameraPosition => cameraPosition;
    
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
        Subscribe();
        timeScaleController.Init();
        computerController.Init();
        computerController.OnWorkDone += SendProgress;
        OnWorkerStateChange += SendWorkerStatus;
        OnPlayerLook += SendPlayerLookStatus;
        computerController.SetWorker(worker);
        IsHasWorker = worker != null;
        IsPlayerLook = false;
    }

    private void Subscribe()
    {
        EventManager.OnPlayModeChange.AddListener(OnPlayModeChange);
        mouseClickDetection.OnMouseClick += OnClicked;
        mouseClickDetection.OnMouseDoubleClick += ActivePersonMode;
       // mouseClickDetection.OnMouseHold += ActiveUIPanel;
        mouseClickDetection.OnMouseDragAtObject += OnHoldMovePerson;
    }

    private void OnHoldMovePerson()
    {
        if (worker == null)
        {
            return;
        }

        IsHasWorker = false;
        SceneController.Instance.SendStartDragWorker(worker);
    }


    private void ActivePersonMode()
    {
        if(!IsHasWorker){return;}
        EventManager.SendOnPlayModeChange(this, true);
    }
    private void OnPlayModeChange(WorkTable workTable, bool isActivePerson)
    {
        mouseClickDetection.SetActive(!isActivePerson);
        if(!this.Equals(workTable)){return;}
        IsPlayerLook = isActivePerson;
    }
    
    private void ActiveUIPanel()
    {
        workTableUI.Show();
    }
    
    private void OnClicked()
    {
        if(IsPlayerLook){return;}
        onClickFeedbacks?.PlayFeedbacks();
    }

    private void SendWorkerStatus(bool isHasWorker)
    {
        computerController.SetWorker(worker);
        if (isHasWorker)
        {
            timeScaleController.SetParameters(worker.GetWorkerDamage());
        }
    }
    private void SendPlayerLookStatus(bool isLook)
    {
        computerController.IsPlayerLook(isLook);
        if (isLook)
        {
            timeScaleController.OnTimeScaleChanged += OnStartWorkFaster;
            timeScaleController.OnRateOverTimeChanged += OnRateChange;
            timeScaleController.Active();
        }
        else
        {
            timeScaleController.OnTimeScaleChanged -= OnStartWorkFaster;
            timeScaleController.OnRateOverTimeChanged -= OnRateChange;
            timeScaleController.Inactive();
        }
    }

    public void SetWorker(Worker worker)
    {
        SceneController.Instance.SendStopDragWorker();
        
        worker.transform.SetParent(workerSeatPosition);
        worker.transform.DOLocalMove(Vector3.zero, workerSeatDuration).OnComplete(() =>
        {
            this.worker = worker;
            IsHasWorker = true;
        });
        
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
    private void OnRateChange(float totalMin, float totalMax,float min, float max,float currentValue)
    {
        computerController.SetRate(totalMin,totalMax,min,max,currentValue);
    }
}
