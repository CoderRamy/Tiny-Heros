using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIMessageDialog : UIBase {
    public TMP_Text textMessage;
    public void Show(string message)
    {
        if (textMessage != null)
            textMessage.text = message;
        Show();
    }
}
