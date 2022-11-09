using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class LevelInProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite completedSprite;
        [SerializeField] private float pulseDuration;
        [SerializeField] private float minPulseAlpha;
        [SerializeField] private float maxPulseAlpha;
        [SerializeField] private float minPulseScale = .9f;
        [SerializeField] private float maxPulseScale = 1.1f;
        [SerializeField] private Ease pulseEase = Ease.InOutCirc;

        private Tween pulseTween, scaleTween;
        private Vector3 startScale;

        private void Awake()
        {
            startScale = icon.transform.localScale;
        }

        public void SetCompleted()
        {
            PulseStop();
            icon.sprite = completedSprite;
        }

        public void SetActive()
        {
            icon.sprite = activeSprite;
            PulseStart();
        }

        public void SetInactive()
        {
            PulseStop();
            icon.sprite = inactiveSprite;
        }

        private void PulseStart()
        {
            PulseStop();
            SetIconAlpha(maxPulseAlpha);
            pulseTween = DOTween.To(GetIconAlpha, SetIconAlpha, minPulseAlpha, pulseDuration)
                .SetEase(pulseEase)
                .SetLoops(-1, LoopType.Yoyo);
            icon.transform.localScale = Vector3.one * minPulseScale;
            scaleTween = icon.transform.DOScale(Vector3.one * maxPulseScale, pulseDuration)
                .SetEase(pulseEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void PulseStop()
        {
            pulseTween?.Kill();
            SetIconAlpha(1f);
            scaleTween?.Kill();
            icon.transform.localScale = startScale;
        }

        private void SetIconAlpha(float value)
        {
            icon.color = icon.color.WithAlpha(value);
        }

        private float GetIconAlpha()
        {
            return icon.color.a;
        }
    }
}