using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLineData : BaseSimplePrimitiveData
{
    public Vector3 fromPoint;
    public Vector3 toPoint;

    public Vector3 GetFromPosition()
    {
        return transform.position + fromPoint;
    }

    public Vector3 GetToPosition()
    {
        return transform.position + toPoint;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawLine(transform.position + fromPoint, transform.position + toPoint);
    }

    public override Vector3 GetRandomPosition()
    {
        var result = Vector3.Lerp(fromPoint, toPoint, Random.value);
        result.x = !isFixRandomX ? result.x : fixRandomX;
        result.y = !isFixRandomY ? result.y : fixRandomY;
        result.z = !isFixRandomZ ? result.z : fixRandomZ;
        return transform.position + result;
    }
}
