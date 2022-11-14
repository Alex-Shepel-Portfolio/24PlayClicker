using System;
using System.Collections;
using System.Collections.Generic;
using TFPlay.UpgradeSystem;
using UnityEngine;

public class Worker : ImprovedMonoBehaviour
{
   [SerializeField] private WorkerParameters workerStartParameters;
   [SerializeField] private WorkerAnimator workerAnimator;
   [SerializeField] private LayerMask workTableLayer;
   [SerializeField] private LayerMask workZoneLayer;
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

   public float GetWorkerID()
   {
      return workerStartParameters.WorkerID;
   }
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

   public void StartMoveWithDrag()
   {
      StartCoroutine(DragCoroutine());
   }

   private IEnumerator DragCoroutine()
   {
      workerAnimator.SetIsDrugAnimation();
      var mainCamera = Camera.main;
      RaycastHit hit;
      Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
      while (Input.GetMouseButton(0))
      {
         ray = mainCamera.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out hit, 10000f, workZoneLayer))
         {
            Vector3 pos = Vector3.Lerp(transform.position, hit.point + Vector3.up, .5f);
            transform.position = new Vector3(pos.x, 2, pos.z);
         }

         yield return null;
      }


      TrySetToTable(ray);
      

      workerAnimator.SetIsWorkingAnimation();
   }

   private void TrySetToTable(Ray ray)
   {
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit, 100f, workTableLayer))
      {
         var workTable = hit.collider.gameObject.GetComponentInParent<WorkTable>();
         if (workTable != null)
         {
            if (!workTable.IsHasWorker)
            {
               workTable.SetWorker(this);
            }
         }
      }
      else
      {
         var table = transform.GetComponentInParent<WorkTable>();
         table.SetWorker(this);

         Debug.Log("CannotFindTable");
      }
   }
}