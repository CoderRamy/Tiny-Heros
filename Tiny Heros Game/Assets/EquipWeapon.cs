using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            //Debug.Log("set Weapons");
            //Destroy(GetComponent<PhotonView>());
            //Destroy(GetComponent<EquipWeapon>());

            if (other.GetComponent<CharacterEntity>())
            {
                Debug.Log("set Weapons Character");
                other.GetComponent<CharacterEntity>().characterModelTransform.GetComponentInChildren<CharacterModel>().SetWeaponModel(null, gameObject, null);
            }

            if (other.GetComponent<BotEntity>())
            {
                Debug.Log("set Weapons Bot");
                other.GetComponent<BotEntity>().characterModelTransform.GetComponentInChildren<CharacterModel>().SetWeaponModel(null, gameObject, null);
            }
        }
    }
}
