using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChangePlayerName : MonoBehaviour
{
    [SerializeField]
    TMP_Text PlayerName;
    public TMP_Text NewPlayerName;
    public GameObject root;

    [SerializeField]
    GameAPIManager gameAPIManager;



    public void Register()
    {
        if (!gameAPIManager.IsPlayerDataRegister)
        {
            Debug.Log("Player Text is : " + PlayerName.text);
            StartCoroutine(gameAPIManager.RegisterRequest(PlayerName.text));
        }
    }
}
