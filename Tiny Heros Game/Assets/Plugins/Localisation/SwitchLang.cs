using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchLang : MonoBehaviour
{
    Button langButton;

    // Start is called before the first frame update
    void Start()
    {
        langButton = GetComponent<Button>();
        langButton.onClick.AddListener(() => { SwitchLanguage(); });

        if (PlayerPrefs.GetInt("language") == 1)
        {
            langButton.GetComponentInChildren<Text>().text = "Arabic";
        }
        if (PlayerPrefs.GetInt("language") == 3)
        {
             langButton.GetComponentInChildren<Text>().text = "English";
        }
    }

    public void SwitchLanguage()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            PlayerPrefs.SetInt("language", 3);
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.Arabic;
            langButton.GetComponentInChildren<Text>().text = "English";
        }
        else if (PlayerPrefs.GetInt("language") == 3)
        {
            PlayerPrefs.SetInt("language", 1);
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.English;
            langButton.GetComponentInChildren<Text>().text = "Arabic";
        }
    }
}
