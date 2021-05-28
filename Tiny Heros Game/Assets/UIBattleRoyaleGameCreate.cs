using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UIBattleRoyaleGameCreate : UIBase
{

    public int maxPlayerCustomizable = 32;
    public UnityScene selectedMap;
    //public InputField inputRoomName;
    //public InputField inputMaxPlayer;
    //[Header("Match Bot Count")]
    //public GameObject containerBotCount;
    //public InputField inputBotCount;
    //[Header("Match Time")]
    //public GameObject containerMatchTime;
    //public InputField inputMatchTime;
    //[Header("Match Kill")]
    //public GameObject containerMatchKill;
    //public InputField inputMatchKill;
    //[Header("Match Score")]
    //public GameObject containerMatchScore;
    //public InputField inputMatchScore;
    //[Header("Maps")]
    //public Image previewImage;

    //public Dropdown mapList;
    //[Header("Game rules")]
    //public Dropdown gameRuleList;

    public BaseNetworkGameRule gameRules;

    public virtual void OnClickCreateGame()
    { 
        var networkManager = SimplePhotonNetworkManager.Singleton;
        var networkGameManager = networkManager as BaseNetworkGameManager;

        if (selectedMap != null)
            networkManager.onlineScene.SceneName = selectedMap.SceneName;

        if (gameRules != null && networkGameManager != null)
        {
            gameRules.botCount = gameRules.DefaultBotCount;
            gameRules.matchTime = gameRules.DefaultMatchTime;
            gameRules.matchKill = gameRules.DefaultMatchKill;
            gameRules.matchScore = gameRules.DefaultMatchScore;
            networkGameManager.gameRule = gameRules;
        }

 
            networkManager.maxConnections = (byte)maxPlayerCustomizable;

        networkManager.CreateRoom();
    }





}
