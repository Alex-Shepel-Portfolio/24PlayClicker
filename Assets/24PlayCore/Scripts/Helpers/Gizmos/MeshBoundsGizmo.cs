using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBounds : MonoBehaviour
{
    [SerializeField] private Collider objCollider;
    [SerializeField] private bool isGet;
    
    private void OnValidate()
    {
        if (isGet)
            return;

        objCollider = GetComponent<Collider>();
        isGet = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(objCollider.bounds.center, objCollider.bounds.size);
    }
}
