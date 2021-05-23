using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeapon : MonoBehaviour
{
    [SerializeField]
    public WeaponData weaponData;

    [SerializeField]
    float CurentTimer;

    [SerializeField]
    float Timer;

    [SerializeField]
    bool IsEquip;

    [SerializeField]
    Image Loader;

    GameObject Character;

    [SerializeField]
    public int Ammo;


    public void Start()
    {
        CurentTimer = Timer;
        Loader.fillAmount = 1;
    }

    public void Update()
    {
        if (IsEquip && CurentTimer > 0)
        {
            CurentTimer -= Time.deltaTime;
            Loader.fillAmount -= 1.0f / Timer * Time.deltaTime;
        }

        if(CurentTimer <= 0)
        {
            equipWeapon();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            IsEquip = true;
            Character = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Character)
        {
            IsEquip = false;
            CurentTimer = Timer;
            Loader.fillAmount = 1;
        }
    }

    void equipWeapon()
    {
        if (Character.GetComponent<CharacterEntity>())
        {
            Debug.Log("set Weapons Character");
            Character.GetComponent<CharacterEntity>().selectWeapon = weaponData.GetId();
        }

        if (Character.GetComponent<BotEntity>())
        {
            Debug.Log("set Weapons Bot");
            Character.GetComponent<BotEntity>().selectWeapon = weaponData.GetId();
            Character.GetComponent<BotEntity>().isPickupWeapon = false;
        }

        if(weaponData.attackType == AttackType.Ranged)
        {
            Ammo = weaponData.OriginAmmo;
        }

        Destroy(transform.parent.gameObject);
    }

}
