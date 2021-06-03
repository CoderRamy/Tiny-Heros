using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCubeData : BaseSimplePrimitiveData
{
    public Vector3 size;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position, size);
    }

    public override Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3(
            !isFixRandomX ? Random.Range(-size.x * 0.5f, size.x * 0.5f) : fixRandomX,
            !isFixRandomY ? Random.Range(-size.y * 0.5f, size.y * 0.5f) : fixRandomY,
            !isFixRandomZ ? Random.Range(-size.z * 0.5f, size.z * 0.5f) : fixRandomZ);
    }
}
