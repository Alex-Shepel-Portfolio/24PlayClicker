using System;

[Serializable]
public class WorkerData
{
    public WorkerStateData SavedWorkerParameters;

    public WorkerData(WorkerStateData parameters)
    {
        SavedWorkerParameters = parameters;
    }
}