using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    public float distance;
    public float decisionRates;

    [SerializeField]
    GameObject TargetObject;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
        {
 
            if (hit.transform.tag == "Weapon")
            {

              //  if(Is_IMelle(GetComponentInChildren<EquipWeapon>())){

                    Debug.Log("Me Weapon Is Melee");
                    Pickup_Behaviour(hit.transform);
                    TargetObject = hit.transform.gameObject;
               // }

            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.white);
          //  Debug.Log("Did not Hit");
        }
    }

    void Pickup_Behaviour(Transform Object)
    {
        GetComponent<BotEntity>().isPickupWeapon = true;
        GetComponent<BotEntity>().targetPosition = Object.position;
    }

    bool Is_IMelle(EquipWeapon PlayerWeapon)
    {
        if(PlayerWeapon.weaponData.attackType == AttackType.Melee)
        {
            return true;
        }

        return false;
    }


    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawRay(transform.position , Vector3.back * distance);
    //}
}




