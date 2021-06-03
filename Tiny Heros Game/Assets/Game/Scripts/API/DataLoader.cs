using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader  {

    public IEnumerator LoadPlayerData()
    {
        WWW PlayerData = new WWW(Env.ApiUrl + "categories"+"/123");
        yield return PlayerData;
        Debug.Log(PlayerData.text);
        PlayerInfoData playerInfo = new PlayerInfoData();
        playerInfo = JsonUtility.FromJson<PlayerInfoData>(PlayerData.text);
        Debug.Log(playerInfo.name);
        yield return playerInfo;
    }




    public IEnumerator LoadProperties(string catid , string family) {

        WWW Properties;

        if(catid == "255")
        {
            Properties = new WWW(Env.ApiUrl + "properties/all/" + family);

        }
        else
        {
            Properties = new WWW(Env.ApiUrl + "properties/" + catid + "/" + family);

        }


        yield return Properties;
        string PropertiesString = Properties.text;
        Debug.Log(PropertiesString);
        Properties properties = new Properties();
        properties = JsonUtility.FromJson<Properties>(PropertiesString);
        yield return properties;
    }

    internal IEnumerator LoadWebsiteSettings()
    {
        WWW website_settings = new WWW(Env.ApiUrl + "website_settings");
        yield return website_settings;
        string website_settingsString = website_settings.text;
        Debug.Log(website_settingsString);
        Website_settings website = new Website_settings();
        website = JsonUtility.FromJson<Website_settings>(website_settingsString);
        yield return website;
        
    }

    public IEnumerator LoadCategories()
    {
        WWW Categories = new WWW(Env.ApiUrl + "categories");
        yield return Categories;
        string CategoriesString = Categories.text;
        Debug.Log(CategoriesString);
        Categories categories = new Categories();
        categories = JsonUtility.FromJson<Categories>(CategoriesString);
      //  Debug.Log(categories.properties[0].title);
        yield return categories;
    }

 

    public IEnumerator LoadGameSettings()
    {
        WWW GameSetting = new WWW(Env.ApiUrl + "game-settings");
        yield return GameSetting;
        string GameSettingString = GameSetting.text;
        GameSettings gameSettings = new GameSettings();
        gameSettings = JsonUtility.FromJson<GameSettings>(GameSettingString);
        yield return gameSettings;
    }

   

    public IEnumerator General_Setting()
    {
        WWW GSetting = new WWW(Env.ApiUrl + "setting/general_setting");
        yield return GSetting;
        string gsettingdata = GSetting.text;
        //General_Setting gsetting = new General_Setting();
        //gsetting = JsonUtility.FromJson<General_Setting>(gsettingdata);
        //yield return gsetting;
    }

}
