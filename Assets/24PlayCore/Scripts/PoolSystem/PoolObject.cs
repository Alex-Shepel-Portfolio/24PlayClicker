using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour
{
    private bool destroyActivated;
    private Transform poolParent;
    private PoolManager.ObjectInstance objectInstance;

    protected virtual void Awake()
    {

    }

    public void SetObjectInstance(PoolManager.ObjectInstance value)
    {
        objectInstance = value;
    }
    
    public void SetPoolParent(Transform parent)
    {
        poolParent = parent;
    }

    public virtual void OnObjectReuse()
    {
        destroyActivated = false;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        transform.SetParent(poolParent);
        PoolManager.Instance.EnqueueObject(objectInstance);
    }

    public void Destroy(float t)
    {
        if (!destroyActivated)
        {
            destroyActivated = true;
            StartCoroutine(DestroyRoutine(t));
        }
    }

    private IEnumerator DestroyRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy();
    }
}
