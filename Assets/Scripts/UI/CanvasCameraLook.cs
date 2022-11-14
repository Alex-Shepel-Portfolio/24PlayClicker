using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraLook : ImprovedMonoBehaviour
{
   private Camera camera;
   private Coroutine lookCameraCoroutine;
   
   public void Init()
   {
      camera = Camera.main;
   }

   public void StartLookAtCamera()
   {
      camera??= Camera.main;
      lookCameraCoroutine = StartCoroutine(LookAtCamera());
   }

   public void StopLookAtCamera()
   {
      StopCoroutine(lookCameraCoroutine);
   }

   private IEnumerator LookAtCamera()
   {
      transform.LookAt(camera.transform.position);
      yield return new WaitForFixedUpdate();
   }
      
}
