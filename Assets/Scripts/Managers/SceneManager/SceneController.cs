using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoSingleton<SceneController>
{
   
    [SerializeField] private WorkTabelsController workTabelsController;
    [SerializeField] private WorkerController workerController;
    private LevelProgressController LevelProgress => LevelProgressController.Instance;
    private CameraController cameraController;
    private InputStationBehavior inputStationBehavior => InputStationBehavior.Instance;

    private WorkTable personModeTable;
    
    public bool IsPreLastStage;
    
    private int currentLevel;
    
    private int CurrentLevel => currentLevel;

    private float StartPointsToNeed;
    private float pointsToNeedDone;
    public float PointsToNeedDone
    {
        get => pointsToNeedDone;
        set
        {
            
            pointsToNeedDone = Mathf.Clamp(value, 0, float.MaxValue);
            OnPointsProgressChange?.Invoke(value);
        }
    }

    public Action<float> OnPointsProgressChange;

    private void OnEnable()
    {
        GameC.Instance.OnLevelLoaded += OnLevelLoad;
    }

    private void OnDisable()
    {
        GameC.Instance.OnLevelLoaded -= OnLevelLoad;
        LevelProgress.OnUpdate -= UpdatePointToNeed;
        OnPointsProgressChange -= CheckWorkStatus;
    }
    private void OnLevelLoad(int _)
    {
        inputStationBehavior.Init(InputStateType.SwipeMoveState);
        cameraController = CameraController.Instance;
        cameraController.Init();
        LevelProgress.OnUpdate += UpdatePointToNeed;
        EventManager.OnWorkDone.AddListener(GetProgress);
        EventManager.OnPlayModeChange.AddListener(OnPlayModeChange);
        OnPointsProgressChange += CheckWorkStatus;
        workerController.Init();
        workTabelsController.Init();
    }

    private void CheckWorkStatus(float workStatus)
    {
        if (workStatus <= 0)
        {
            SendWorkDone();
        }
    }

    private void UpdatePointToNeed(LevelProgress levelProgress)
    {
        PointsToNeedDone = LevelProgress.GetPointerPerStage();
        StartPointsToNeed = LevelProgress.GetPointerPerStage();
        IsPreLastStage = levelProgress.CurrentStage == levelProgress.NumberStages - 1;
    }

    private void GetProgress(float progres)
    {
        PointsToNeedDone -= progres;
        MenuUI.Instance.LevelProgressBar.SetProgressValue(PointsToNeedDone, StartPointsToNeed);
    }

    public void SendWorkDone()
    {
        LevelProgress.SendStageFinish();
    }

    private void OnPlayModeChange(WorkTable workTable, bool isPersonMode)
    {
        personModeTable = workTable;
        if (isPersonMode)
        {
            ActivePersonMode(workTable.CameraPosition);
        }
        else
        {
            ActiveGlobalMode();
        }
    }


    public void ActivePersonMode(Transform cameraPosition)
    {
        cameraController.MovePersonCamera(cameraPosition);
        cameraController.PersonLookCamera();
        inputStationBehavior.SwitchState(InputStateType.ClickerState);
        MenuUI.Instance.ActiveExitPersonModeButton();
    }
    public void ActiveGlobalMode()
    {
        cameraController.GlobalLookCamera();
        inputStationBehavior.SwitchState(InputStateType.SwipeMoveState);
    }

    public void ExitPersonMode()
    {
        EventManager.SendOnPlayModeChange(personModeTable, false);
    }

    public void SendStartDragWorker(Worker worker)
    {
        inputStationBehavior.SwitchState(InputStateType.MinZoneMoveState);
        worker.StartMoveWithDrag();
    }
    public void SendStopDragWorker()
    {
        inputStationBehavior.SwitchState(InputStateType.SwipeMoveState);
    }
}
