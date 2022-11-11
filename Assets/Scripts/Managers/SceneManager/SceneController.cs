using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoSingleton<SceneController>
{
    [SerializeField] private WorkTabelsController workTabelsController;
    [SerializeField] private WorkerController workerController;
    private LevelProgressController LevelProgress => LevelProgressController.Instance;
    
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
        LevelProgress.OnUpdate += UpdatePointToNeed;
        EventManager.OnWorkDone.AddListener(GetProgress);
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


    
}
