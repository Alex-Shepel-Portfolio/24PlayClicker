using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgressBar : ImprovedMonoBehaviour
{
   [SerializeField] private FillBar fillBar;
   [SerializeField] private TMP_Text levelText;
   [SerializeField] private TMP_Text stageText;
   [SerializeField] private TMP_Text valueToNeedText;
   [SerializeField] private TMP_Text currentValueText;

   private void Start()
   {
      LevelProgressController.Instance.OnUpdate += SetNewParameters;
   }
   
   public void SetProgressValue(float currentValue, float targetValue)
   {
      valueToNeedText.SetText($"Need =>{targetValue}");
      currentValueText.SetText($"Current =>{currentValue}");
      fillBar.FillStatus(currentValue, targetValue);
   }

   private void SetNewParameters(LevelProgress levelParameters)
   {
      levelText.SetText($"Level {levelParameters.Level}");
      stageText.SetText($"Stage {levelParameters.CurrentStage} / {levelParameters.NumberStages}");
   }
}
