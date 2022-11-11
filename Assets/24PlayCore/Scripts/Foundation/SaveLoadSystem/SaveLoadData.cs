using System;
using TFPlay.UpgradeSystem;

[Serializable]
public class SaveLoadData
{
    public GameData Game;
    public SettingsData Settings;
    public UpgradeData WorkerUpgrades;
    public WorkerBaseData WorkerData;

    public SaveLoadData()
    {
        Game = new GameData();
        Settings = new SettingsData();
        WorkerUpgrades = new UpgradeData();
        WorkerData = new WorkerBaseData();
    }
}