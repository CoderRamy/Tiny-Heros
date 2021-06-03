using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Localisation.Creator
{
    public class SerializeJSON : MonoBehaviour
    {
        private string directoryPath;
        private string jsonName;
        public static SerializeJSON Instance;
        // Use this for initialization

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            directoryPath = DataPath;
        }


        public void SaveData(string fileName, string jsonData)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                AlertFactory.DisplayAlert("Please enter the filename to save the level");
                Debug.LogError("No file name entered");
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            jsonName = fileName;
            File.WriteAllText(path, jsonData);
            AlertFactory.DisplayAlert((string.Format("<B>{0}</B> saved at \n <B>{1}</B>", fileName, path)));
        }



        public string LoadData(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                AlertFactory.DisplayAlert("Please enter the filename to load the data");
                Debug.LogError("No file name entered");
                return null;
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            jsonName = fileName;

            if (!File.Exists(path))
            {
                AlertFactory.DisplayAlert("File not found. \n Tip : Ensure you have added .json as an entension.");
                Debug.LogError(" file not found");
                return null;
            }
            return System.IO.File.ReadAllText(path);
        }

        public string path
        {
            get
            {
                return Path.Combine(DataPath, jsonName);
            }
        }

        private string DataPath
        {
            get
            {
                return Application.dataPath + "/Localisation/Languages";
            }
        }
    }
}
