using System;

[Serializable]
public class GameData
{
    public StoredValue<int> Level;
    public StoredValue<int> Coins;
    public StoredValue<LevelProgress> ProgressLevel;

    public GameData()
    {
        Level = new StoredValue<int>(1);
        Coins = new StoredValue<int>(0);
        ProgressLevel = new StoredValue<LevelProgress>(new LevelProgress{Level = 1,NumberStages = 3, CurrentStage = 0});
    }
}