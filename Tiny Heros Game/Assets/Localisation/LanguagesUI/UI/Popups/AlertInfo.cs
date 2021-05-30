using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AlertInfo : MonoBehaviour
{

    public Action<string> callback = null;
    public Text msgLbl;

    //For one button only.
    public void Init(string message)
    {
        msgLbl.text = message;

        gameObject.SetActive(true);
    }

    //For two buttons, other one is always used to close the popup.
    public void Init(string message, Action<string> callback)
    {
        Init(message);
        this.callback = callback;
    }

    public void OnBtnPressed()
    {
        if (this.callback != null)
        {
            this.callback.Invoke("Yeah");
        }
    }

    public void OnBackBtnPressed()
    {
        this.callback = null;
        gameObject.SetActive(false);
    }
}
