using UnityEngine;
using TFPlay.UI;

namespace TFPlay.UpgradeSystem
{
    public class UpgradeMenuUI : BaseCanvasGroupUI
    {
        [SerializeField]
        private UpgradeButtonUI[] upgradeButtons;
        [SerializeField]
        private int showFromLevel = 2;

        protected override void Start()
        {
            base.Start();
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                upgradeButtons[i].OnPurchased += UpgradeButtonUI_OnPurchased;
            }
            GameC.Instance.OnLevelStartLoading += OnLevelStartLoading;
            GameC.Instance.OnLevelEnd += OnLevelEnd;
        }

        private void OnLevelEnd(bool levelSuccess)
        {
            HideInstant();
        }

        private void OnLevelStartLoading(int level)
        {
            if (level >= showFromLevel)
            {
                Show();
            }
            else
            {
                HideInstant();
            }
        }

        private void UpgradeButtonUI_OnPurchased(UpgradeType upgradeType)
        {
            UpgradeController.Instance.Upgrade(upgradeType);
        }
    }
}
