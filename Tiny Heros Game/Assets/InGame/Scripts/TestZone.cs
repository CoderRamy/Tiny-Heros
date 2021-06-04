using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZone : MonoBehaviour
{

    Vector3 currentCenterPosition;
    Vector3 viewportPoint;
    Vector3 radiusViewportPoint;
    BRGameplayManager brGameManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var brGameManager = GameplayManager.Singleton as BRGameplayManager;

         currentCenterPosition = brGameManager.currentCenterPosition;
         viewportPoint = Camera.main.WorldToViewportPoint(currentCenterPosition);
         radiusViewportPoint = Camera.main.WorldToViewportPoint(currentCenterPosition + (Vector3.one * brGameManager.currentRadius));

    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, brGameManager.currentCircle);
    //}
}
