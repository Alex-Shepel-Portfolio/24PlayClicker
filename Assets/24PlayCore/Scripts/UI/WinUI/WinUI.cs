using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TFPlay.UI
{
    public class WinUI : BaseEndScreenUI
    {
        [SerializeField] protected Button continueButton;
        [SerializeField] protected List<ParticleSystem> effects;


        protected override void Init()
        {
            base.Init();
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            continueButton.interactable = true;
            PlayEffects();
        }

        public override void Hide()
        {
            base.Hide();
            continueButton.interactable = false;
        }

        protected virtual void OnContinueButtonClicked()
        {
            Hide();
            GameC.Instance.NextLevel();
            Taptic.Selection();
        }

        private void PlayEffects()
        {
            foreach (var effect in effects)
            {
                effect.Play();
            }
        }
    }
}