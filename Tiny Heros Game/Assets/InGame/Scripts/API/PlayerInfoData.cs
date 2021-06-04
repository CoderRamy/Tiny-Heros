using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfoData
{
    public int status;
    public Player player;
}

[System.Serializable]

public class Player
{
    public int id;
    public string name;
    public string device_id;
    public int facebook_id;
    public int google_id;
    public int apple_id;
    public int gold;
    public int gem;
    public int level;
    public float exp;
    public string status;
    public string token;
}
