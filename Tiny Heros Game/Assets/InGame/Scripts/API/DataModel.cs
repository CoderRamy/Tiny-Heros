using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataModel : MonoBehaviour {

    void ShowApiError()
    {
         UINetworkClientError.Singleton.messageDialog.Show("\n Server Connection Error  \n ( APIS )");
    }

    public IEnumerator DataBase_Register(string name)
    {
        Debug.Log("Player db name is : " + name);
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("device_id", SystemInfo.deviceUniqueIdentifier);
        WWW www = new WWW(Env.ApiUrl + "player/register", form);
        yield return www;

        //if (www.error.Length > 0)
        //{
        //    ShowApiError();
        //}
  
        string callback_data = www.text;
        Debug.Log("DataBase_Register" + callback_data);
        PlayerInfoData CallBack = new PlayerInfoData();
        CallBack = JsonUtility.FromJson<PlayerInfoData>(callback_data);
        yield return CallBack;
       
    }



    public IEnumerator DataBase_Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("device_id", SystemInfo.deviceUniqueIdentifier);
        WWW www = new WWW(Env.ApiUrl + "player/login", form);
        yield return www;

        //if(www.error.Length > 0)
        //{
        //    ShowApiError();
        //}
  
        string callback_data = www.text;
        Debug.Log("DataBase_Login" + callback_data);
        PlayerInfoData CallBack = new PlayerInfoData();
        CallBack = JsonUtility.FromJson<PlayerInfoData>(callback_data);
        yield return CallBack;
       
    }



    public void DataBase_ContactUs(string name, string email, string mobile,string subject, string body)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("email", email);
        form.AddField("mobile", mobile);
        form.AddField("subject", subject);
        form.AddField("body", body);
        form.AddField("did", SystemInfo.deviceUniqueIdentifier);
#if UNITY_EDITOR
        form.AddField("appid", "the Challenge " + EditorUserBuildSettings.activeBuildTarget.ToString());
#else
                        form.AddField("appid", "the Challenge "+Application.platform.ToString());
#endif
        form.AddField("code", PlayerPrefs.GetString("code"));
        WWW www = new WWW("https://mybarq.com/Iphone/form.php", form);

    }

 

    public IEnumerator DataBase_AddPoints(string gameName, int userID,string userName, int points, string status,  string type)
    {
        WWWForm form = new WWWForm();
        form.AddField("game", gameName);
        form.AddField("user_id", userID);
        form.AddField("user_name", userName);
        form.AddField("points", points);
        form.AddField("status", status);
        form.AddField("type", type);
        WWW www = new WWW(Env.ApiUrl + "addPoints", form);
        yield return www;
        string callback_data = www.text;
        APICallBack CallBack = new APICallBack();
        CallBack = JsonUtility.FromJson<APICallBack>(callback_data);
        yield return CallBack;
    }

    public IEnumerator DataBase_addGamePoints(int userID, int points)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userID);
        form.AddField("points", points);
        WWW www = new WWW(Env.ApiUrl + "addGamePoints", form);
        yield return www;
        string callback_data = www.text;
        APICallBack CallBack = new APICallBack();
        CallBack = JsonUtility.FromJson<APICallBack>(callback_data);
        yield return CallBack;
    }

    public IEnumerator DataBase_addDealyPoints(int userID, string userName, int points)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", userID);
        form.AddField("user_name", userName);
        form.AddField("points", points);
        WWW www = new WWW(Env.ApiUrl + "addDealyPoints", form);
        yield return www;
        string callback_data = www.text;
        APICallBack CallBack = new APICallBack();
        CallBack = JsonUtility.FromJson<APICallBack>(callback_data);
        yield return CallBack;
    }
}
