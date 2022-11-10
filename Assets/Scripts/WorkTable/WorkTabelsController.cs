using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTabelsController : ImprovedMonoBehaviour
{
    [SerializeField] private WorkTable[] workTables;

    public void Init()
    {
        InitTables();
    }
    
    private void InitTables()
    {
        foreach (var workTable in workTables)
        {
            workTable.Init();
        }
    }
}
