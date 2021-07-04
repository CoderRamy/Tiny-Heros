using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameSetting
{
    public string name;
    public int single_mode_points;
    public int online_mode_points;
    public int ai_mode_points;
    public int single_mode_gems;
    public int online_mode_gems;
    public int ai_mode_gems;
    public float game_timer;
    public float turn_timer;
    public int turns;
}

public class GameSettings
{
    public GameSetting[] gameSettings;
}
