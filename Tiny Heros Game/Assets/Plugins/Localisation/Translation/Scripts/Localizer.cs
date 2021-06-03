using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Localizer : MonoBehaviour
{
    //Static Vars
    public static Localizer instance;
    //Delegates and events
    public delegate void LanguageChangedDelegate();

    public event LanguageChangedDelegate OnLanguageChanged;

    //Enum things, add languages here.
    public enum SupportedLanguage
    {
        English = 1,
        French,
        Arabic
    }

    [SerializeField]
    private SupportedLanguage _currentLanguage = SupportedLanguage.English;

    public SupportedLanguage CurrentLanguage
    {
        get { return _currentLanguage; }
        set
        {
            _currentLanguage = value;

            LoadLocalizedData(CurrentLanguage);

            if (OnLanguageChanged != null)
            {
                OnLanguageChanged.Invoke();
            }
        }
    }

    private Dictionary<string, string> localizedText;

    public bool IsReady { get; private set; }

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        CurrentLanguage = _currentLanguage;

    }

    private void LoadLocalizedData(SupportedLanguage language)
    {
        IsReady = false;

        string filePath = string.Format("Languages/{0}", language.ToString().ToLower());
        TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));

        if (txt != null)
        {
            localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt.text);
            //Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        IsReady = true;
    }

    private void LoadLocalizedData(string fileName)
    {
        IsReady = false;

        string filePath = "Languages/" + fileName;
        TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));

        if (txt != null)
        {
            localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt.text);
            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        IsReady = true;
    }

    public string GetLocalizedValue(string key, bool isMultipleLine = false)
    {
        string result = key.Trim();
        if (localizedText == null)
            return result;

        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];

            if (CurrentLanguage == SupportedLanguage.Arabic)
            {
                if (!isMultipleLine)
                    result = ArabicSupport.ArabicFixer.Fix(result, true);

            }
            return result;
        }
        Debug.Log(string.Format("Key: '{0}' not found!", key));
        return string.Format("Key: '{0}' not found!", key);
    }
}
