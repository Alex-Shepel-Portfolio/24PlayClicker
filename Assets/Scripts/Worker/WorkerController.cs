using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoSingleton<WorkerController>
{
    [SerializeField] private Worker[] activeWorkers;
    
    
    public void Init()
    {
        foreach (var activeWorker in activeWorkers)
        {
            activeWorker.Init();
        }
    }
}
