using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameAPIManager : MonoBehaviour
{

    public static GameAPIManager instance;

    DataLoader dataLoader = new DataLoader();
    DataModel dataModel = new DataModel();
    [SerializeField]
    public PlayerInfoData playerData;
    [SerializeField]
    GameObject PlayerModel;

    [HideInInspector]
    public bool IsPlayerDataRegister = false;
    [HideInInspector]
    public bool IsPlayerDataLogin = false;

    [SerializeField]
    UIChangePlayerName UIChanngePlayerName;
    [SerializeField]
    UIMainMenuScene UIMainMenu;

   

    public void Awake()
    {
        instance = this;
        StartCoroutine(LoginRequest());
    }

    //get player data from database
    public void Register() 
    {
            PlayerModel.SetActive(false);
            UIChanngePlayerName.root.SetActive(true);
    }

    //register new player
    public IEnumerator RegisterRequest(string PlayerName)
    {
        Debug.Log("Player name is : " + PlayerName);
        CoroutineWithData cd = new CoroutineWithData(this, dataModel.DataBase_Register(PlayerName));
        yield return cd.coroutine;

        playerData = cd.result as PlayerInfoData;
        IsPlayerDataRegister = true;
        SetPlayerData();
        Debug.Log("Player Register Loaded" + playerData.player.name);
    }

    //login and get player data
    public IEnumerator LoginRequest()
    {
        CoroutineWithData cd = new CoroutineWithData(this, dataModel.DataBase_Login());
        yield return cd.coroutine;

        playerData = cd.result as PlayerInfoData;
        IsPlayerDataLogin = true;

        if(playerData.status == 2)
        {
            Register();
        }
        else
        {
            SetPlayerData();
        }
    }

    void SetPlayerData()
    {
        PlayerSave.SetPlayerName(playerData.player.name);
        PlayerModel.SetActive(true);
        UIChanngePlayerName.NewPlayerName.text = playerData.player.name;
        UIChanngePlayerName.root.SetActive(false);
        UIMainMenu.LoadMenu();
        Debug.Log("Player Login Loaded" + playerData.player.name);
    }

    public void Restart()
    {
        Application.LoadLevel("Main");
    }

}
