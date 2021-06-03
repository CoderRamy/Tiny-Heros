using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocText : MonoBehaviour
{
    [SerializeField] string key;
    Text text;

    [HideInInspector]
    public int index = 0;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (Localizer.instance)
        {
            Localizer.instance.OnLanguageChanged += LocalizeText;
        }

        StartCoroutine(ReflectLanguage());
    }

    private IEnumerator ReflectLanguage()
    {
        while (Localizer.instance == null)
        {
            yield return null;
        }
        while (!Localizer.instance.IsReady)
        {
            yield return null;
        }
        LocalizeText();
    }

    private void OnDisable()
    {

        if (Localizer.instance)
        {
            Localizer.instance.OnLanguageChanged -= LocalizeText;
        }
    }

    public void LocalizeText()
    {
        text.text = Localizer.instance.GetLocalizedValue(key);
    }

    public void SetKey(string key)
    {
        this.key = key;
    }
    public Text GetText
    {
        get
        {
            if (text == null) text = GetComponent<Text>();
            return text;
        }
    }
}
