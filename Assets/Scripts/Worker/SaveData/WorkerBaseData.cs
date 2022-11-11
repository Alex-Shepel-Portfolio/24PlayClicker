using System;
using System.Collections.Generic;

[Serializable]
public class WorkerBaseData
{
    private Dictionary<int, WorkerData> WorkerUpgrades;

    public WorkerBaseData()
    {
        WorkerUpgrades = new Dictionary<int, WorkerData>();
    }

    public WorkerData this[int id]
    {
        get
        {
            if (WorkerUpgrades.ContainsKey(id))
            {
                return WorkerUpgrades[id];
            }
            var defaultValue = new WorkerStateData { IsActive = false};
            Register(id, defaultValue);
            return WorkerUpgrades[id];
        }
    }

    public void TryRegisterWorker(int id, WorkerStateData workerParameters )
    {
        if (WorkerUpgrades.ContainsKey(id))
        {
            return;
        }

        Register(id, workerParameters);
    }

    public void RegisterWorker(int id, WorkerStateData workerParameters) => Register(id, workerParameters);

    private void Register(int id, WorkerStateData workerParameters)
    {
        WorkerUpgrades.Add(id, new WorkerData(workerParameters));
    }
}