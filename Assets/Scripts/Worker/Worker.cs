using System;
using System.Collections;
using System.Collections.Generic;
using TFPlay.UpgradeSystem;
using UnityEngine;

public class Worker : ImprovedMonoBehaviour
{
   [SerializeField] private WorkerParameters workerStartParameters;
   [SerializeField] private WorkerAnimator workerAnimator;
   private int workerID;
   
   public void Init()
   {
      workerID = workerStartParameters.WorkerID;
      Registration();
   }

   public void Registration()
   {
      SLS.Data.WorkerData.TryRegisterWorker(workerID, new WorkerStateData{IsActive = true});
      SLS.Data.WorkerUpgrades.TryRegisterWorker(workerID, workerStartParameters.startCostValue);
   }

   public void ChangeAnimationSpeed(float speed) => workerAnimator.ChangeAnimationSpeed(speed);

   public float GetWorkerDamage()
   {
      var calculateDamageWithUpdate = workerStartParameters.Damage * UpgradeController.Instance.GetDamage(workerID);
      return calculateDamageWithUpdate;
   }
   public float GetWorkerWriteSpeed()
   {
      var calculateWriteSpeedWithUpdate = workerStartParameters.WriteSpeed * UpgradeController.Instance.GetWriteSpeed(workerID);
      return calculateWriteSpeedWithUpdate;
   }
}