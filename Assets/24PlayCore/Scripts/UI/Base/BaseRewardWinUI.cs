using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public abstract class BaseRewardWinUI : WinUI
    {
        [SerializeField] private Button rewardButton;
        [SerializeField] private Button noThanksButton;
        [SerializeField] private float noThanksButtonShowDelay = 4f;

        private CoroutineItem noThanksButtonCoroutineItem;

        protected override void Init()
        {
            base.Init();
            rewardButton.onClick.AddListener(OnRewardButtonClicked);
            noThanksButton.onClick.AddListener(NoThanksButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            rewardButton.interactable = true;
            rewardButton.SetActive();
            noThanksButton.SetInactive();
            continueButton.SetInactive();
            noThanksButtonCoroutineItem = this.WaitAndDoCoroutine(noThanksButtonShowDelay, noThanksButton.SetActive);
        }

        public override void Hide()
        {
            base.Hide();
            rewardButton.interactable = false;
        }

        protected virtual void NoThanksButtonClicked()
        {
            OnContinueButtonClicked();
        }

        protected virtual void OnRewardedAdShown()
        {
            continueButton.SetActive();
            rewardButton.SetInactive();
            noThanksButtonCoroutineItem.Stop();
            noThanksButton.SetInactive();
        }

        protected abstract void OnRewardButtonClicked();
    }
}
