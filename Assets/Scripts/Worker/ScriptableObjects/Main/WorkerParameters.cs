using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="WorkerParameters",menuName="CustomTools/WorkerParameters/WorkerParameters", order = 1)]
public class WorkerParameters : ScriptableObject
{
    public int WorkerID;
    public float Damage;
    public float WriteSpeed = 1;
    
}
