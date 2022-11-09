using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class LoseUI : BaseEndScreenUI
    {
        [SerializeField] protected Button restartButton;

        protected override void Init()
        {
            base.Init();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            restartButton.interactable = true;
        }

        public override void Hide()
        {
            base.Hide();
            restartButton.interactable = false;
        }
        
        protected virtual void OnRestartButtonClicked()
        {
            Hide();
            GameC.Instance.RestartLevel();
            Taptic.Selection();
        }
    }
}