using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPooledUpdate
{
    void Update();
}

public class PooledUnityCalls : MonoBehaviour
{
    private static PooledUnityCalls instance;
    private static List<IPooledUpdate> activeUpdates = new List<IPooledUpdate>();
    private static Queue<IPooledUpdate> queuedAddUpdates = new Queue<IPooledUpdate>();
    private static Queue<IPooledUpdate> queuedRemoveUpdates = new Queue<IPooledUpdate>();
    private static bool isUpdating = false;

    public static void AddUpdate(IPooledUpdate update)
    {
        if(!activeUpdates.Contains(update))
        {
            if (isUpdating)
            {
                queuedAddUpdates.Enqueue(update);
            }
            else
            {
                activeUpdates.Add(update);
            }
        }
    }

    public static void RemoveUpdate(IPooledUpdate update)
    {
        if (isUpdating)
        {
            queuedRemoveUpdates.Enqueue(update);
        }
        else
        {
            activeUpdates.Remove(update);
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple instances of singleton! Destroying offending component.");
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        isUpdating = true;

        for(int i = 0; i < activeUpdates.Count; i++)
        {
            activeUpdates[i].Update();
        }

        isUpdating = false;

        while(queuedAddUpdates.Count > 0)
        {
            var update = queuedAddUpdates.Dequeue();
            if(!activeUpdates.Contains(update))
            {
                activeUpdates.Add(update);
            }
        }

        while (queuedRemoveUpdates.Count > 0)
        {
            var update = queuedRemoveUpdates.Dequeue();
            activeUpdates.Remove(update);
        }
    }
}
