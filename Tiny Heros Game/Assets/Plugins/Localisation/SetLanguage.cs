using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour
{

    void Awake()
    {

        if (!PlayerPrefs.HasKey("language"))
        {
            PlayerPrefs.SetInt("language", 1);
        }
    }

      void Start()
      {
    //    PlayerPrefs.SetInt("language", 3);

        if (PlayerPrefs.GetInt("language") == 1)
        {
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.English;
        }
        else
        {
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.Arabic;
        }

        Debug.Log("CurrentLanguage " + Localizer.instance.CurrentLanguage);
    }

    void UpdateLanguage(int language)
    {

        PlayerPrefs.SetInt("language",language);

        if (PlayerPrefs.GetInt("language") == 1)
        {
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.English;
        }
        else
        {
            Localizer.instance.CurrentLanguage = Localizer.SupportedLanguage.Arabic;
        }
    }

}
