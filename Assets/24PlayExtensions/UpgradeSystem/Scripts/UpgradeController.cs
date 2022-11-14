using UnityEngine;
using UnityEngine.Serialization;

namespace TFPlay.UpgradeSystem
{
    public class UpgradeController : MonoSingleton<UpgradeController>
    {
        [SerializeField] private int maxUpgradesCount = 100;
        [SerializeField] private float writeSpeedPerLevel = 1f;
        [SerializeField] private AnimationCurve writeSpeedProgressionCurve;
        [SerializeField] private float stressTolerancePerLevel = 1f;
        [SerializeField] private AnimationCurve stressTolerancehProgressionCurve;
        [SerializeField] private float damageMultiplierPercent = 0.1f;
        [SerializeField] private AnimationCurve damageProgressionCurve;
        [Space(5)] [SerializeField] private UpgradeMenuUI upgradeMenuUI;

        private Transform target;

        private bool TargetExists => target != null;

        public event System.Action<Transform> OnTargetInitialized;
        public event System.Action<UpgradeType> OnUpgraded;

        public void ActiveMenuUI()
        {
            upgradeMenuUI.Show();
        }
        public void InactiveMenuUI()
        {
            upgradeMenuUI.Hide();
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
            OnTargetInitialized?.Invoke(target);
        }

        public void Upgrade(UpgradeType upgradeType)
        {
            if (!TargetExists)
                return;

            OnUpgraded?.Invoke(upgradeType);
        }

        public float GetWriteSpeed(int workerID)
        {
            return GetUpgradeValue(workerID,UpgradeType.WriteSpeed, writeSpeedProgressionCurve, writeSpeedPerLevel);
        }

        public float GetStressTolerance(int workerID)
        {
            return GetUpgradeValue(workerID,UpgradeType.StressTolerance, stressTolerancehProgressionCurve, stressTolerancePerLevel);
        }

        public float GetDamage(int workerID)
        {
            return GetUpgradeValue(workerID,UpgradeType.Damage, damageProgressionCurve, damageMultiplierPercent);
        }
        private int GetUpgradeLevel(int workerID, UpgradeType upgradeType)
        {
            var upgradeLevel = SLS.Data.WorkerUpgrades[workerID].Upgrades[upgradeType].Level;
            upgradeLevel = Mathf.Clamp(upgradeLevel, 0, maxUpgradesCount);
            return upgradeLevel;
        }
        
        private float GetUpgradeValue(int workerID ,UpgradeType upgradeType, AnimationCurve curve, float multiplier)
        {
            var level = GetUpgradeLevel(workerID,upgradeType);
            var value = 0f;
            for (int i = 0; i < level; i++)
            {
                var progressionMultiplier = curve.Evaluate((float)i / maxUpgradesCount);
                value += progressionMultiplier * multiplier;
            }
            return value;
        }
    }
}
