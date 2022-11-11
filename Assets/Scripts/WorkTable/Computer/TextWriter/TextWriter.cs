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
   [SerializeField] private RectTransform textHolder;
   [SerializeField] private TMP_Text textAtScreen;
   [SerializeField] private TextPresetsToWrite textPresetsToWrite;
   [SerializeField] private float writeSpeed = 1f;
   [SerializeField] private float workerSpeedWrite = 1f;
   [SerializeField] private float duration = 1f;

   [Space(5), Header("animationParameters")] 
   [SerializeField] private SendTextAnimationParameters sendTextAnimationParameters;
   [SerializeField] private SendBuildAnimationParameters sendBuildAnimationParameters;
   
   public Action OnComplete;

   private string currentWriteText = String.Empty;



   private Coroutine writeTextCoroutine;
   private TweenerCore<string, string, StringOptions> textWriterTween;

   private TextPreset currentWritePreset;

   public void SetRate(float min, float max,float currentValue)
   {
      textAtScreen.color = Color.Lerp(Color.green, Color.red, Mathf.InverseLerp(min, max, currentValue));
   }
   public void UpdateWriteTime(float writeTime)
   {
      writeSpeed = writeTime;
      if(textWriterTween==null){return;}
      textWriterTween.timeScale = writeSpeed;
   }
   
   public void StartWriteText(float workerSpeedWrite,bool isNeedSendBuild)
   {
      if(writeTextCoroutine != null){return;}
      var textPreset = GetTextPreset();
      writeTextCoroutine = StartCoroutine(WriteTextCoroutine(currentWriteText, textPreset, workerSpeedWrite, isNeedSendBuild));
   }
   public void StopWriteText()
   {
      if(writeTextCoroutine == null){return;}
    StopCoroutine(writeTextCoroutine);
    writeTextCoroutine = null;
   }


   private IEnumerator WriteTextCoroutine(string currentText, TextPreset textPreset, float workerSpeedWrite, bool isNeedSendBuild)
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
      textWriterTween.OnComplete(() => IsCompleteWriting(isNeedSendBuild));
      yield return null;
   }

   private void IsCompleteWriting(bool isNeedSendBuild)
   {
      if (isNeedSendBuild)
      {
         StartCoroutine(SendBuildAnimation());
         return;
      }
      StartCoroutine(SendTextAnimation());
   }
   private IEnumerator SendBuildAnimation()
   {
      Sequence sendTextSequence = DOTween.Sequence();
      sendBuildAnimationParameters.letterObject.localPosition = Vector3.zero;
      var scaleAnimationTime = sendBuildAnimationParameters.duration * sendBuildAnimationParameters.scaleOffset;
      var sendAnimationTime = sendBuildAnimationParameters.duration - scaleAnimationTime;
      sendTextSequence.Append(textHolder.DOScale(sendBuildAnimationParameters.endScale,
         sendBuildAnimationParameters.duration));
      sendTextSequence.Join(sendBuildAnimationParameters.letterObject.DOScale(1,
         sendBuildAnimationParameters.duration).SetEase(sendBuildAnimationParameters.TextScaleEase).OnComplete(()=>textHolder.localScale = Vector3.zero));
      sendTextSequence.Append(sendBuildAnimationParameters.letterObject.DOLocalMoveY(sendBuildAnimationParameters.endYPosition,
         sendAnimationTime).SetEase(sendBuildAnimationParameters.MoveUpEase));
      sendTextSequence.OnComplete(() => ResetText());
      yield return null;
   }

   private IEnumerator SendTextAnimation()
   {
      Sequence sendTextSequence = DOTween.Sequence();
      sendTextSequence.Append(textHolder.DOLocalMoveY(sendTextAnimationParameters.endYPosition,
         sendTextAnimationParameters.duration).SetEase(sendTextAnimationParameters.MoveUpEase));
      sendTextSequence.OnComplete(() => ResetText());
      yield return null;
   }
   private void ResetText()
   {
      currentWritePreset = new TextPreset { TextToWrite = String.Empty, WriteSpeedOffset = 0 };
      currentWriteText = String.Empty;
      textAtScreen.SetText(currentWriteText);
      writeTextCoroutine = null;
      textHolder.localPosition = Vector3.zero;
      textHolder.localScale = Vector3.one;
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

[Serializable]
public struct SendTextAnimationParameters
{
   public float endYPosition;
   public Ease MoveUpEase;
   public float duration;
}
[Serializable]
public struct SendBuildAnimationParameters
{
   public float endYPosition;
   public float endScale;
   public Transform letterObject;
   public Ease MoveUpEase;
   public Ease TextScaleEase;
   public float scaleOffset;
   public float duration;
}
