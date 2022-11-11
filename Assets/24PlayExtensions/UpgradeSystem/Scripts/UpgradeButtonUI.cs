using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TFPlay.UpgradeSystem
{
    public class UpgradeButtonUI : MonoBehaviour, IButtonClickEffectReceiver
    {
        [SerializeField]
        private int workerID;
        [SerializeField]
        private Image background;
        [SerializeField]
        private Sprite activeSprite;
        [SerializeField]
        private Sprite inactiveSprite;
        [SerializeField]
        private Button button;
        [SerializeField]
        private TextMeshProUGUI levelText;
        [SerializeField]
        private TextMeshProUGUI costText;
        [SerializeField]
        private Image upgradeIcon;
        [SerializeField]
        private Image coinIcon;
        [SerializeField]
        private Image upgradeTipImage;
        [SerializeField]
        private UpgradeType upgradeType;
        [SerializeField]
        private int upgradeCostPerLevel = 25;

        private Upgrade UpgradeData => SLS.Data.WorkerUpgrades[workerID].Upgrades[upgradeType];

        public event System.Action<UpgradeType> OnPurchased;
        public event System.Action<bool> OnClicked;

        private void Start()
        {
            button.onClick.AddListener(OnButtonClicked);
            SLS.Data.Game.Coins.OnValueChanged += Coins_OnValueChanged;
            OnPurchased += Level_OnValueChanged;
            OnPurchased += Cost_OnValueChanged;

            Cost_OnValueChanged(upgradeType);
            Level_OnValueChanged(upgradeType);
            Coins_OnValueChanged(SLS.Data.Game.Coins.Value);
        }

        private void Cost_OnValueChanged(UpgradeType upgradeType)
        {
            costText.text =  UpgradeData.Cost.ToString();
        }

        private void Level_OnValueChanged(UpgradeType upgradeType)
        {
            levelText.text = string.Format("Level {0}",  UpgradeData.Level);
        }

        private void Coins_OnValueChanged(int coins)
        {
            var hasEnoughMoney = HasEnoughMoney();
            SetInteractable(hasEnoughMoney);
        }

        public void SetInteractable(bool canInteract)
        {
            upgradeTipImage.gameObject.SetActive(canInteract);
            background.sprite = canInteract ? activeSprite : inactiveSprite;
        }

        public bool HasEnoughMoney()
        {
            return SLS.Data.Game.Coins.Value >= UpgradeData.Cost;
        }

        private void OnButtonClicked()
        {
            var hasEnoughMoney = HasEnoughMoney();
            if (hasEnoughMoney)
            {
                Upgrade();
            }
            OnClicked?.Invoke(hasEnoughMoney);
        }

        private void Upgrade()
        {
            var currentUpgradeCost = UpgradeData.Cost;
            UpgradeData.Level+= 1;
            UpgradeData.Cost += upgradeCostPerLevel;
            SLS.Data.Game.Coins.Value -= currentUpgradeCost;
            SLS.Save();
            OnPurchased?.Invoke(upgradeType);
        }
    }
}
