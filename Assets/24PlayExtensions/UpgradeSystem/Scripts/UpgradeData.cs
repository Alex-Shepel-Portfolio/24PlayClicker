using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace TFPlay.UpgradeSystem
{
    [Serializable]
    public class UpgradeData
    {
        private Dictionary<int, WorkerUpgrade> WorkerUpgrades;
        public UpgradeData()
        {
            WorkerUpgrades = new Dictionary<int, WorkerUpgrade>();
        }
        
        public WorkerUpgrade this[int id]
        {
            get
            {
                if (WorkerUpgrades.ContainsKey(id))
                {
                    return WorkerUpgrades[id];
                }
                
                var defaultCostValue = new UpgradeStartCost { Damage = GameResources.UpgradesValue.StartDamageCost,
                    StressTolerance = GameResources.UpgradesValue.StartStressTolerantCost,
                    WriteSpeed = GameResources.UpgradesValue.StartWriteSpeedCost };
                Register(id, defaultCostValue);
                return WorkerUpgrades[id];
            }
        }

        public void TryRegisterWorker(int id, UpgradeStartCost startCost)
        {
            if (WorkerUpgrades.ContainsKey(id)){return;}
            Register(id,startCost);
        }
        public void RegisterWorker(int id,UpgradeStartCost startCost) => Register(id,startCost);

        private void Register(int id, UpgradeStartCost startCost)
        {
            WorkerUpgrades.Add(id, new WorkerUpgrade(startCost));
        }
    }
    [Serializable]
    public class WorkerUpgrade
    {
        public WorkerUpgradeData Upgrades;
        public WorkerUpgrade(UpgradeStartCost startCost)
        {
            Upgrades = new WorkerUpgradeData(startCost);
        }
    }
    [Serializable]
    public class WorkerUpgradeData
    {
        private Dictionary<UpgradeType, Upgrade> Upgrades;
        public Upgrade WriteSpeed;
        public Upgrade StressTolerance;
        public Upgrade Damage;

        public WorkerUpgradeData(UpgradeStartCost startCost) 
        {
            Damage = new Upgrade(1, startCost.Damage);
            WriteSpeed = new Upgrade(1, startCost.WriteSpeed);
            StressTolerance = new Upgrade(1, startCost.StressTolerance);
            Upgrades = new Dictionary<UpgradeType, Upgrade>();
            Upgrades[UpgradeType.WriteSpeed] = WriteSpeed;
            Upgrades[UpgradeType.StressTolerance] = StressTolerance;
            Upgrades[UpgradeType.Damage] = Damage;
        }
        public Upgrade this[UpgradeType type]
        {
            get => Upgrades[type];
        }
    }

    [Serializable]
    public class Upgrade
    {
        
        public int Level;
        public int Cost;

        public Upgrade(int level, int cost)
        {
            Level = level;
            Cost =  cost;
        }
    }
    
    [Serializable]
    public struct UpgradeStartCost
    {
        public int WriteSpeed;
        public int StressTolerance;
        public int Damage;
       
    }
    
  
}
