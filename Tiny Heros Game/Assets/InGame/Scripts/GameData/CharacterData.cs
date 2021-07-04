using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ItemData
{
    public CharacterModel modelObject;

    [Header("Database")]
    [SerializeField]
    int ID;

    [SerializeField]
    public int MaxCards = 10;

    [SerializeField]
    public CharactersRank charactersRank;
    
}


public enum CharactersRank
{
    Normal = 0,
    Magic,
    Rare,
    Unique,
}
