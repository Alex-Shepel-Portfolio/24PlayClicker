using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class LevelUI : BaseUIBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private string levelFormat = "Level {0}";
        [SerializeField] private Button restartButton;

        protected override void Init()
        {
            base.Init();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            GameC.Instance.OnLevelLoaded += OnLevelLoaded;
            GameC.Instance.OnLevelStartLoading += OnLevelStartLoading;
        }
        
        private void OnLevelLoaded(int levelNumber)
        {
            restartButton.interactable = true;
            SetLevel(levelNumber);
        }

        private void OnLevelStartLoading(int levelNumber)
        {
            restartButton.interactable = false;
        }

        private void SetLevel(int sceneNumber)
        {
            levelText.text = string.Format(levelFormat, SLS.Data.Game.Level.Value);
        }

        private void OnRestartButtonClicked()
        {
            GameC.Instance.RestartLevel();
            Taptic.Selection();
        }
    }
}