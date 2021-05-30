using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Localisation.Creator
{

    public class LocalisedGen : MonoBehaviour
    {
        // public int gridSize = 10;
        public InputField ipPrefab;
        public InputField[] inputs;
        private int max;
        public InputField tagInp;
        public InputField fileNameInp;
        public ClueHolder mAcross;

        void Init()
        {
            inputs = new InputField[max];
        }

        public void SaveLevelInfo()
        {
            DataDictionary cd = new DataDictionary();
            for (int i = 0; i < mAcross._clues.Count; i++)
            {
                cd.AddData(mAcross._clues[i].number.text, mAcross._clues[i].data.text);
            }
            string jsonData = JsonConvert.SerializeObject(cd.DataStorage, Formatting.Indented);
            if (string.IsNullOrEmpty(fileNameInp.text))
            {
                AlertFactory.DisplayAlert("The filename is empty. \nPlease fill them too !!");
                return;
            }
            string fileName = fileNameInp.text.ToLower();
            SerializeJSON.Instance.SaveData(fileName, jsonData);
        }

        public void CleanData()
        {
            mAcross.Clear();
            fileNameInp.text = System.String.Empty;
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].text = System.String.Empty;
            }
	  
        }


        public void LoadData()
        {
            string fileName = fileNameInp.text;
            CleanData();
            fileNameInp.text = fileName;
            FillUIData();
        }

        private void FillUIData()
        {
            if (string.IsNullOrEmpty(fileNameInp.text))
            {
                AlertFactory.DisplayAlert("The LETTERS are empty. \nPlease fill them too !!");
                return;
            }
            string fileName = fileNameInp.text;
            string text = SerializeJSON.Instance.LoadData(fileName);
            Dictionary<string,string> jsonValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            mAcross.PopulateData(jsonValues);
            AlertFactory.DisplayAlert(string.Format("{0} loaded successfully", fileNameInp.text));
        }

        void Start()
        {
            Init();
        }
    }
}

