using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlertFactory : MonoBehaviour
{
    private static AlertFactory _instance;
    private static AlertFactory instance {
        get{
            if (_instance == null)
            {
                _instance = FindObjectOfType<AlertFactory>();
            }
            return _instance;
        }
    }

    public AlertInfo alertDialog;
    public AlertInfo confirmation;

    public static void DisplayAlert(string msg)
    {
        if (instance.alertDialog == null)
            return;
        instance.alertDialog.Init(msg);
    }

    public static void DisplayConfirmation(string msg, Action<string> callback)
    {
        if (instance.confirmation == null)
            return;
        instance.confirmation.Init(msg, callback);
    }
}
