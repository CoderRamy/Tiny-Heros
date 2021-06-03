using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{

    // Use this for initialization
    public float speed = 100;
    public Renderer renderer;
    public Vector3 rotationVector;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = renderer.bounds.center;

        transform.RotateAround(position, rotationVector, speed * Time.deltaTime);

      //  transform.Rotate(0, speed * Time.deltaTime, 0,Space.World);
    }
}