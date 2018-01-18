using UnityEngine;
using System.Collections;

public class OurMonoBehaviour : MonoBehaviour, IPooledUpdate
{
    protected Transform cachedTransform;

    protected Collider cachedCollider;
    protected bool triedCachingCollider = false;

    protected Rigidbody cachedRigidbody;
    protected bool triedCachingRigidbody = false;

    public new Transform transform
    {
        get
        {
            if (cachedTransform == null)
                cachedTransform = base.transform;

            return cachedTransform;
        }
    }

    public new Collider collider
    {
        get
        {
            if (!triedCachingCollider)
            {
                cachedCollider = base.GetComponent<Collider>();
                if(cachedCollider == null)
                {
                    cachedCollider = base.GetComponentInChildren<Collider>();
                }
                triedCachingCollider = true;
            }

            return cachedCollider;
        }
    }

    public new Rigidbody rigidbody
    {
        get
        {
            if (!triedCachingRigidbody)
            {
                cachedRigidbody = GetComponent<Rigidbody>();
                if (cachedRigidbody == null)
                {
                    cachedRigidbody = base.GetComponentInChildren<Rigidbody>();
                }
                triedCachingRigidbody = true;
            }

            return cachedRigidbody;
        }
    }

    public virtual void OnEnable()
    {
        PooledUnityCalls.AddUpdate(this);
    }

    public virtual void OnDisable()
    {
        PooledUnityCalls.RemoveUpdate(this);
    }

    public virtual void Update()
    {

    }
}
