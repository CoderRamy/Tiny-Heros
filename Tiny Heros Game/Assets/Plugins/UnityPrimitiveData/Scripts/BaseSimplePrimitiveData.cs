using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSimplePrimitiveData : MonoBehaviour
{
    public bool isFixRandomX;
    public float fixRandomX;
    public bool isFixRandomY;
    public float fixRandomY;
    public bool isFixRandomZ;
    public float fixRandomZ;
    public Color gizmosColor = Color.magenta;
    public abstract Vector3 GetRandomPosition();
}
