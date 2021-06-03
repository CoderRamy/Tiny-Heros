using System.Collections.Generic;

namespace Localisation.Creator
{
    [System.Serializable]
    public class DataDictionary
    {
        public Dictionary<string, string> DataStorage = new Dictionary<string, string>();

        public void AddData(string key, string value)
        {
            try
            {
                DataStorage.Add(key.ToUpper(), value);
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.Log("Key error " + key);
            }

        }
    }

}