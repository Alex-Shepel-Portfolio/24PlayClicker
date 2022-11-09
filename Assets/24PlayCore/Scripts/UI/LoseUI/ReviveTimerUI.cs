using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TFPlay.UI
{
    public class ReviveTimerUI : MonoBehaviour
    {
        [SerializeField]
        private Image timerBar;
        [SerializeField]
        private TextMeshProUGUI timerText;
        [SerializeField]
        private float reviveSeconds = 10;

        private IEnumerator timerRoutine;

        public void StartTimer()
        {
            StopTimer();
            timerRoutine = TimerRoutine();
            StartCoroutine(timerRoutine);
        }

        public void StopTimer()
        {
            if (timerRoutine != null)
            {
                StopCoroutine(timerRoutine);
            }
        }

        private void SetTimerText(float remaimingTime)
        {
            timerText.text = remaimingTime.ToString();
        }

        private void ShakeTimerText()
        {
            timerText.transform.DOShakeScale(0.5f, 0.5f, 10, 0f);
        }

        private void FillTimerBar(float endValue, float duration)
        {
            if (duration > 0)
            {
                timerBar.DOFillAmount(endValue, duration).SetEase(Ease.Linear);
            }
            else
            {
                timerBar.fillAmount = endValue;
            }
        }

        private IEnumerator TimerRoutine()
        {
            var delay = new WaitForSeconds(1f);
            var remaimingTime = reviveSeconds;
            SetTimerText(remaimingTime);
            FillTimerBar(remaimingTime, 0f);
            yield return new WaitForSeconds(0.5f);
            while (remaimingTime > 0)
            {
                remaimingTime--;
                FillTimerBar(remaimingTime / reviveSeconds, 1f);
                yield return delay;
                SetTimerText(remaimingTime);
                ShakeTimerText();
            }
        }
    }
}
