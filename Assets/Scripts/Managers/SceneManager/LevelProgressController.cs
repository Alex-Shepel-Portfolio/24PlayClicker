using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct LevelProgress
{
    public int Level;
    public int NumberStages;
    public int CurrentStage;
}
public class LevelProgressController : MonoSingleton<LevelProgressController>
{
    [SerializeField] private int maxLevelValue = 100;
    [SerializeField] private float perLevel = 1f;
    [SerializeField] private AnimationCurve speedProgressionCurve;

    public event System.Action OnLevelFinish;
    public event System.Action OnLastStage;
    public event System.Action OnStageUp;
    public event System.Action<LevelProgress> OnUpdate;

    private LevelProgress currentProgressLevel;

    private void Start()
    {
        SLS.Data.Game.ProgressLevel.OnValueChanged += SetNewLevelValue;
    }

    public void SendStageFinish()
    {
        var stage = currentProgressLevel.CurrentStage++;
        if (stage == currentProgressLevel.NumberStages)
        {
            OnLastStage?.Invoke(); 
        }
        else if (stage > currentProgressLevel.NumberStages)
        {
            CreateNextLevel();
            OnLevelFinish?.Invoke();
            return;
        }
        NextStage();
    }

    private void NextStage()
    {
        var oldValue = SLS.Data.Game.ProgressLevel.Value;
        var LevelProgress = new LevelProgress
        {
            Level = oldValue.Level,
            NumberStages = oldValue.NumberStages,
            CurrentStage = oldValue.CurrentStage+1
        };
        
        SLS.Data.Game.ProgressLevel.Value = LevelProgress;
        SendNeedUpgrade();
        OnUpdate?.Invoke(LevelProgress);
    }

    private void CreateNextLevel()
    {
        var oldValue = SLS.Data.Game.ProgressLevel.Value;
        var newLevelNumber = oldValue.Level + 1;
        var newStageNumber = GetNumberOfStages(newLevelNumber);
        var LevelProgress = new LevelProgress
        {
            Level = newLevelNumber,
            NumberStages = newStageNumber,
            CurrentStage = 1
        };
        
        SLS.Data.Game.ProgressLevel.Value = LevelProgress;
        SendNeedUpgrade();
    }
    private void SendNeedUpgrade()
    {
        OnUpdate?.Invoke(SLS.Data.Game.ProgressLevel.Value);
    }

    private int GetNumberOfStages(int level)
    {
        return 5;
    }

    public int GetPointerPerStage()
    {
        return CalculatePoinerPerStage();
    }

    private int CalculatePoinerPerStage()
    {
        var numberStages = currentProgressLevel.NumberStages;
        var currentStage = currentProgressLevel.CurrentStage;
        var pointsValueAtLevel = GetProgressLevel();
        var pointsPerLevel = Mathf.RoundToInt(pointsValueAtLevel / (numberStages / currentStage));
        return pointsPerLevel;
    }

    public float GetProgressLevel()
    {
        return GetLevelValue(speedProgressionCurve, perLevel);
    }
    
    private void SetNewLevelValue(LevelProgress levelProgress)
    {
        currentProgressLevel = levelProgress;
    }
    
    private LevelProgress GetUpgradeLevel()
    {
        var upgradeLevel = SLS.Data.Game.ProgressLevel.Value;
        return upgradeLevel;
    }

    private float GetLevelValue( AnimationCurve curve, float multiplier)
    {
        var level = currentProgressLevel.Level;
        var value = 0f;
        for (int i = 0; i < level; i++)
        {
            var progressionMultiplier = curve.Evaluate((float)i / maxLevelValue);
            value += progressionMultiplier * multiplier;
        }

        return value;
    }
}
