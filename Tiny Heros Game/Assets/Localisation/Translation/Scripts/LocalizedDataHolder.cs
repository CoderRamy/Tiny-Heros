using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class LocalizedDataHolder
{

    private static Dictionary<string,string> localizedText = new Dictionary<string, string>();
    private static List<string> _keys = new List<string>();

    public static void Refresh()
    {
        localizedText.Clear();
        _keys.Clear();
        GetKeys();
    }

    public static List<string> GetKeys()
    {
        if (_keys.Count == 0 && localizedText.Count==0)
        {
            string filePath = string.Format("Languages/{0}", "EngLish".ToString().ToLower());
            TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
            localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(txt.text);

            foreach (var item in localizedText)
            {
                _keys.Add(item.Key);
            }
        }

        return _keys;
    }
}
