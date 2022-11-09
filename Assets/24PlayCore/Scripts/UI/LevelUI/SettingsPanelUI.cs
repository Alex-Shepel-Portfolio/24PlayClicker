﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TFPlay.UI
{
    public class SettingsPanelUI : BaseSettingsUI
    {
        [Header("Settings Panel")]
        [SerializeField] protected bool showRestorePurchasesButton;
        [SerializeField] protected bool showPrivacyPolicyButton;
        [SerializeField] private CanvasGroup settingsHolder;
        [SerializeField] private Button closeSettingsButton;
        [SerializeField] private Button restorePurchasesButton;
        [SerializeField] private Button privacyPolicyButton;

        [Header("Animation Settings")]
        [SerializeField]
        private Ease fadeEase = Ease.InOutCubic;
        [SerializeField]
        private float fadeDuration = 0.5f;
        [SerializeField]
        private float maxBackgroundFadeAlpha = 0.75f;

        protected override void OnValidate()
        {
            base.OnValidate();
            soundButton.gameObject.SetActive(showSoundButton);
            vibrationButton.gameObject.SetActive(showVibrationButton);
            restorePurchasesButton.gameObject.SetActive(showRestorePurchasesButton);
            privacyPolicyButton.gameObject.SetActive(showPrivacyPolicyButton);
        }

        protected override void Init()
        {
            base.Init();

            closeSettingsButton.onClick.AddListener(OnCloseSettingsButtonClicked);
            restorePurchasesButton.onClick.AddListener(OnRestorePurchasesButtonClicked);
            privacyPolicyButton.onClick.AddListener(OnPrivacyPolicyButtonClicked);

#if UNITY_ANDROID && !UNITY_EDITOR
            restorePurchasesButton.SetInactive();
#endif
        }

        private void OnCloseSettingsButtonClicked()
        {
            TogglePanel();
            Taptic.Selection();
        }

        private void OnRestorePurchasesButtonClicked()
        {
            Taptic.Selection();
        }

        private void OnPrivacyPolicyButtonClicked()
        {
            Taptic.Selection();
        }

        protected override void TogglePanel()
        {
            IsOpened = !IsOpened;
            if (IsOpened)
            {
                settingsHolder.DOFade(0f, fadeDuration).From(1f).SetEase(fadeEase);
                background.DOFade(0f, fadeDuration).From(maxBackgroundFadeAlpha).SetEase(fadeEase).OnComplete(() =>
                {
                    settingsHolder.SetInactive();
                    background.SetInactive();
                });
            }
            else
            {

                settingsHolder.SetActive();
                settingsHolder.alpha = 1f;
                settingsHolder.transform.DOScale(1f, fadeDuration).From(0.5f).SetEase(fadeEase);
                background.SetActive();
                background.DOFade(maxBackgroundFadeAlpha, fadeDuration).From(0f).SetEase(fadeEase);
            }
        }
    }
}