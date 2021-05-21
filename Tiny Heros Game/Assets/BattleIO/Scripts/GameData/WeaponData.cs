﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WeaponData : ItemData
{
    public GameObject rightHandObject;
    public GameObject leftHandObject;
    public GameObject shieldObject;
    public List<AttackAnimation> attackAnimations;
    public DamageEntity damagePrefab;
    [Header("SFX")]
    public AudioClip[] attackFx;
    public readonly Dictionary<int, AttackAnimation> AttackAnimations = new Dictionary<int, AttackAnimation>();

    public WeaponRank weaponRank;
    public AttackType attackType;
    public int OriginAmmo;

    public void Launch(CharacterEntity attacker, bool isLeftHandWeapon)
    {
        if (attacker == null || !PhotonNetwork.isMasterClient)
            return;

        var gameNetworkManager = GameNetworkManager.Singleton;
        var gameplayManager = GameplayManager.Singleton;
        var spread = attacker.TotalSpreadDamages;
        var damage = (float)attacker.TotalAttack;
        damage += Random.Range(gameplayManager.minAttackVaryRate, gameplayManager.maxAttackVaryRate) * damage;

        var addRotationX = 0f;
        var addRotationY = 0f;
        var addingRotationY = 360f / spread;
        
        if (spread <= 16)
        {
            addRotationY = (-(spread - 1) * 15f);
            addingRotationY = 30f;
        }

        for (var i = 0; i < spread; ++i)
        {
            Transform launchTransform;
            attacker.GetDamageLaunchTransform(isLeftHandWeapon, out launchTransform);
            // An transform's rotation, position will be set when set `Attacker`
            // So don't worry about them before damage entity going to spawn
            // Velocity also being set when set `Attacker` too.
            var position = launchTransform.position;
            var direction = attacker.TempTransform.forward;
            var damageEntity = DamageEntity.InstantiateNewEntity(damagePrefab, isLeftHandWeapon, position, direction, attacker.photonView.viewID, addRotationX, addRotationY);
            damageEntity.weaponDamage = Mathf.CeilToInt(damage);
            gameNetworkManager.photonView.RPC("RpcCharacterAttack", PhotonTargets.Others, GetId(), isLeftHandWeapon, position, direction, attacker.photonView.viewID, addRotationX, addRotationY);
            addRotationY += addingRotationY;
        }

        attacker.photonView.RPC("RpcEffect", PhotonTargets.All, attacker.photonView.viewID, CharacterEntity.RPC_EFFECT_DAMAGE_SPAWN);
    }

    public void SetupAnimations()
    {
        foreach (var attackAnimation in attackAnimations)
        {
            AttackAnimations[attackAnimation.actionId] = attackAnimation;
        }
    }

    public AttackAnimation GetRandomAttackAnimation()
    {
        var list = AttackAnimations.Values.ToList();
        var randomedIndex = Random.Range(0, list.Count - 1);
        return list[randomedIndex];
    }
}


public enum WeaponRank
{
    Bronze = 0,
    Silver,
    Gold,
    Platinum,
    Diamond,
}

public enum AttackType
{
    Melee = 0,
    Ranged,
}