﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : BaseNetworkGameInstance
{
    public static GameInstance Singleton { get; private set; }
    public CharacterEntity characterPrefab;
    public BotEntity botPrefab;
    public CharacterData[] characters;
    public HeadData[] heads;
    public WeaponData[] weapons;
    public BotData[] bots;
    [Tooltip("Physic layer for characters to avoid it collision")]
    public int characterLayer = 8;
    public bool showJoystickInEditor = true;
    public string watchAdsRespawnPlacement = "respawnPlacement";
    // An available list, list of item that already unlocked
    public static readonly List<HeadData> AvailableHeads = new List<HeadData>();
    public static readonly List<CharacterData> AvailableCharacters = new List<CharacterData>();
    public static readonly List<WeaponData> AvailableWeapons = new List<WeaponData>();
    // All item list
    public static readonly Dictionary<string, HeadData> Heads = new Dictionary<string, HeadData>();
    public static readonly Dictionary<string, CharacterData> Characters = new Dictionary<string, CharacterData>();
    public static readonly Dictionary<string, WeaponData> Weapons = new Dictionary<string, WeaponData>();
    protected override void Awake()
    {
        base.Awake();
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(gameObject);
        Physics.IgnoreLayerCollision(characterLayer, characterLayer, true);

        Heads.Clear();
        foreach (var head in heads)
        {
            Heads[head.GetId()] = head;
        }

        Characters.Clear();
        foreach (var character in characters)
        {
            Characters[character.GetId()] = character;
        }

        Weapons.Clear();
        foreach (var weapon in weapons)
        {
            weapon.SetupAnimations();
            Weapons[weapon.GetId()] = weapon;
        }

        UpdateAvailableItems();
        ValidatePlayerSave();
    }

    public void ValidatePlayerSave()
    {
        var head = PlayerSave.GetHead();
        if (head < 0 || head >= AvailableHeads.Count)
            PlayerSave.SetHead(0);

        var character = PlayerSave.GetCharacter();
        if (character < 0 || character >= AvailableCharacters.Count)
            PlayerSave.SetCharacter(0);

        var weapon = PlayerSave.GetWeapon();
        if (weapon < 0 || weapon >= AvailableWeapons.Count)
            PlayerSave.SetWeapon(0);
      
    }

    public void UpdateAvailableItems()
    {
        AvailableHeads.Clear();
        foreach (var helmet in heads)
        {
            if (helmet != null && helmet.IsUnlock())
                AvailableHeads.Add(helmet);
        }

        AvailableCharacters.Clear();
        foreach (var character in characters)
        {
            if (character != null && character.IsUnlock())
                AvailableCharacters.Add(character);
        }

        AvailableWeapons.Clear();
        foreach (var weapon in weapons)
        {
            if (weapon != null && weapon.IsUnlock())
                AvailableWeapons.Add(weapon);
        }
    }

    public static HeadData GetHead(string key)
    {
        if (Heads.Count == 0)
            return null;
        HeadData result;
        Heads.TryGetValue(key, out result);
        return result;
    }

    public static CharacterData GetCharacter(string key)
    {
        if (Characters.Count == 0)
            return null;
        CharacterData result;
        Characters.TryGetValue(key, out result);
        return result;
    }

    public static WeaponData GetWeapon(string key)
    {
        if (Weapons.Count == 0)
            return null;
        WeaponData result;
        Weapons.TryGetValue(key, out result);
        return result;
    }

    public static HeadData GetAvailableHead(int index)
    {
        if (AvailableHeads.Count == 0)
            return null;
        if (index <= 0 || index >= AvailableHeads.Count)
            index = 0;
        
        return AvailableHeads[index];
    }

    public static CharacterData GetAvailableCharacter(int index)
    {
        if (AvailableCharacters.Count == 0)
            return null;
        if (index <= 0 || index >= AvailableCharacters.Count)
            index = 0;
        return AvailableCharacters[index];
    }

    public static WeaponData GetAvailableWeapon(int index)
    {
        if (AvailableWeapons.Count == 0)
            return null;
        if (index <= 0 || index >= AvailableWeapons.Count)
            index = 0;
        return AvailableWeapons[index];
    }
}
