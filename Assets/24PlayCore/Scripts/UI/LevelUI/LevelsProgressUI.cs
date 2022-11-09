using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFPlay.UI
{
    public class LevelsProgressUI : MonoBehaviour
    {
        [SerializeField] private LevelInProgressBarUI commonLevelPrefab;
        [SerializeField] private LevelInProgressBarUI lastLevelPrefab;
        [SerializeField] private int levelsCount = 7;

        private List<LevelInProgressBarUI> levels = new List<LevelInProgressBarUI>();

        private void Start()
        {
            CreateLevels();
            GameC.Instance.OnLevelLoaded += Refresh;
            GameC.Instance.OnLevelStarted += Hide;
        }

        private void Hide()
        {
            gameObject.SetInactive();
        }

        private void Show()
        {
            gameObject.SetActive();
        }
        
        private void Refresh(int sceneId)
        {
            Refresh();
        }

        private void Refresh()
        {
            var currentLevel = (SLS.Data.Game.Level.Value - 1) % levelsCount;

            for (var i = 0; i < levels.Count; i++)
            {
                if (i < currentLevel)
                    levels[i].SetCompleted();
                else if (i == currentLevel)
                    levels[i].SetActive();
                else
                    levels[i].SetInactive();
            }
            
            Show();
        }

        private void CreateLevels()
        {
            var width = (levelsCount - 1) * commonLevelPrefab.GetRectTransform().rect.width +
                        lastLevelPrefab.GetRectTransform().rect.width;
            var halfWidth = width / 2;
            var oneItemX = width / levelsCount;
            var oneItemHalfWidth = oneItemX / 2f;

            for (var i = 0; i < levelsCount - 1; i++)
            {
                var level = Instantiate(commonLevelPrefab, transform);
                level.GetRectTransform().localPosition = new Vector3(-halfWidth + oneItemX * i + oneItemHalfWidth, 0, 0);
                levels.Add(level);
            }

            var lastLevel = Instantiate(lastLevelPrefab, transform);
            lastLevel.GetRectTransform().localPosition = new Vector3(halfWidth - oneItemX + oneItemHalfWidth, 0, 0);
            levels.Add(lastLevel);

            Refresh();
        }
    }
}