using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

public class TextWriter : ImprovedMonoBehaviour
{
   [SerializeField] private TMP_Text textAtScreen;
   [SerializeField] private TextPresetsToWrite textPresetsToWrite;
   [SerializeField] private float writeSpeed = 1f;
   [SerializeField] private float workerSpeedWrite = 1f;
   [SerializeField] private float duration = 1f;
   
   public Action OnComplete;

   private string currentWriteText = String.Empty;



   private Coroutine writeTextCoroutine;
   private TweenerCore<string, string, StringOptions> textWriterTween;

   private TextPreset currentWritePreset;

   
   
   public void UpdateWriteTime(float writeTime)
   {
      writeSpeed = writeTime;
      textWriterTween.timeScale = writeSpeed;
   }
   
   public void StartWriteText(float workerSpeedWrite)
   {
      if(writeTextCoroutine != null){return;}
      var textPreset = GetTextPreset();
      writeTextCoroutine = StartCoroutine(WriteTextCoroutine(currentWriteText, textPreset, workerSpeedWrite));
   }
   public void StopWriteText()
   {
      if(writeTextCoroutine == null){return;}
    StopCoroutine(writeTextCoroutine);
    writeTextCoroutine = null;
   }

   private IEnumerator WriteTextCoroutine(string currentText, TextPreset textPreset, float workerSpeedWrite)
   {
      string textToWrite =RemoveWroteText(currentText, textPreset.TextToWrite);
      duration = Mathf.Clamp(textToWrite.Length / workerSpeedWrite, 0.1f, 10000);
      WaitForSeconds waitTime = new WaitForSeconds(duration);
      textWriterTween = DOTween.To(() => currentText, x => currentText = x, textToWrite, duration);
      textWriterTween.OnUpdate(() =>
      {
         currentWriteText = currentText;
         textAtScreen.SetText(currentText);
      });
      textWriterTween.OnComplete(() => ResetText());
      yield return null;
   }

   private void ResetText()
   {
      currentWritePreset = new TextPreset { TextToWrite = String.Empty, WriteSpeedOffset = 0 };
      currentWriteText = String.Empty;
      textAtScreen.SetText(currentWriteText);
      writeTextCoroutine = null;
      OnComplete?.Invoke();
   }

   private string RemoveWroteText(string wroteText, string text)
   {
      if (String.IsNullOrEmpty(wroteText))
      {
         return text;
      }

      text.Replace(wroteText," ");
      return text;
   }

   private TextPreset GetTextPreset()
   {
      if (!String.IsNullOrEmpty(currentWritePreset.TextToWrite))
      {
         return currentWritePreset;
      }
      currentWritePreset = textPresetsToWrite.GetRandomPreset();
      return currentWritePreset;
   }
}
