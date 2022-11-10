using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : ImprovedMonoBehaviour
{
   [SerializeField] private WorkerParameters workerStartParameters;

   public float GetWorkerDamage()
   {
      var calculateDamageWithUpdate = workerStartParameters.Damage /*UpgradePersonDamage*/;
      return calculateDamageWithUpdate;
   }
   public float GetWorkerWriteSpeed()
   {
      var calculateWriteSpeedWithUpdate = workerStartParameters.WriteSpeed /*UpgradePersonDamage*/;
      return calculateWriteSpeedWithUpdate;
   }
}
