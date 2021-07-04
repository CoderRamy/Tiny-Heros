using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINetworkClientError : MonoBehaviour
{
    public static UINetworkClientError Singleton { get; private set; }
    public UIMessageDialog messageDialog;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Singleton = this;
        SimplePhotonNetworkManager.onConnectionError += OnConnectionError;
        SimplePhotonNetworkManager.onRoomConnectError += OnRoomConnectError;
    }

    public void OnConnectionError(DisconnectCause error)
    {
        if (messageDialog == null)
            return;

        messageDialog.Show(error.ToString());
    }

    public void OnRoomConnectError(object[] codeAndMsg)
    {
        if (messageDialog == null)
            return;

        messageDialog.Show(codeAndMsg[1].ToString() + "\n(" + codeAndMsg[0].ToString() + ")");
    }
}
