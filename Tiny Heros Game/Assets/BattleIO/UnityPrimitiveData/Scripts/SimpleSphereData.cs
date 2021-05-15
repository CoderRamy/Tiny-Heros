using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSphereData : BaseSimplePrimitiveData
{
    public float radius;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public override Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3(
            !isFixRandomX ? Random.Range(-radius, radius) : fixRandomX,
            !isFixRandomY ? Random.Range(-radius, radius) : fixRandomY,
            !isFixRandomZ ? Random.Range(-radius, radius) : fixRandomZ);
    }
}
