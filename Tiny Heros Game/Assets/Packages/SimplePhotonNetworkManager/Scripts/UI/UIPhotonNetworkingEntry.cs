using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class UIPhotonNetworkingEntry : UnityEngine.MonoBehaviour
{
    public Text textRoomName;
    public Text textPlayerName;
    public Text textSceneName;
    public Text textPlayerCount;
    private NetworkDiscoveryData _data;
    public void SetData(NetworkDiscoveryData data)
    {
        _data = data;
        if (textRoomName != null)
            textRoomName.text = string.IsNullOrEmpty(data.roomName) ? "Untitled" : data.roomName;
        if (textPlayerName != null)
            textPlayerName.text = data.playerName;
        if (textSceneName != null)
            textSceneName.text = data.sceneName;
        if (textPlayerCount != null)
            textPlayerCount.text = data.numPlayers + "/" + data.maxPlayers;
    }

    public virtual void OnClickJoinButton()
    {
        var networkManager = SimplePhotonNetworkManager.Singleton;
        networkManager.JoinRoom(_data.name);
    }
}
