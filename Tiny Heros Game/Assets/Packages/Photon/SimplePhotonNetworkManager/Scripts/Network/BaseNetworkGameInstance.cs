using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class BaseNetworkGameInstance : MonoBehaviour
{
    public BaseNetworkGameRule[] gameRules;
    public static Dictionary<string, BaseNetworkGameRule> GameRules = new Dictionary<string, BaseNetworkGameRule>();
    protected virtual void Awake()
    {
        GameRules.Clear();
        foreach (var gameRule in gameRules)
        {
            GameRules[gameRule.name] = gameRule;
        }
    }

    protected virtual void Start()
    {
        // The photon version not supports arguments
    }
}
