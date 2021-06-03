using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenuScene : MonoBehaviour
{
    public GameObject root;
    [SerializeField]
    TMP_Text Name;
    [SerializeField]
    TMP_Text Gold;
    [SerializeField]
    TMP_Text Gem;
    [SerializeField]
    TMP_Text XP;
    [SerializeField]
    TMP_Text Level;
    public void LoadMenu()
    {
        Name.text = GameAPIManager.instance.playerData.player.name;
        Gold.text = GameAPIManager.instance.playerData.player.gold.ToString();
        Gem.text = GameAPIManager.instance.playerData.player.gem.ToString();
        XP.text = GameAPIManager.instance.playerData.player.exp.ToString();
        Level.text = GameAPIManager.instance.playerData.player.level.ToString(); ;
    }


}
