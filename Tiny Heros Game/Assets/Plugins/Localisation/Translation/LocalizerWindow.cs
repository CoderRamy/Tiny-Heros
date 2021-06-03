using System.Collections.Generic;
using System.IO;
using Localisation.Creator;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class LocalizerWindow : EditorWindow
{
    public string fileName = "english.json";
    private Vector2 scrollPos = new Vector2(7, 125);

    public class LocDataItem
    {
        public string Key, Value;

        public LocDataItem()
        {
            this.Value = this.Key = System.String.Empty;
        }

        public LocDataItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public List<LocDataItem> _localisedDataList = new List<LocDataItem>();
    private bool refreshAssetDatabase;

    [MenuItem("Localisation/View data")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(LocalizerWindow));
    }

    private void OnGUI()
    {

        GUILayout.Label("Localisation Data Editor", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        fileName = EditorGUILayout.TextField("Filename", fileName);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            SaveChangesToFile();
        }
        if (GUILayout.Button("Clear data"))
        {
            _localisedDataList.Clear();
        }

        if (GUILayout.Button("Load file"))
        {
            string _path = LoadDataEditor(fileName);
            if (!string.IsNullOrEmpty(_path))
            {
                _localisedDataList.Clear();
                Dictionary<string, string> collection = JsonConvert.DeserializeObject<Dictionary<string, string>>(_path);
                foreach (var item in collection)
                {
                    _localisedDataList.Add(new LocDataItem(item.Key, item.Value));
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        //to show the data from set piece.

        EditorGUILayout.BeginVertical(EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add entry"))
        {
            _localisedDataList.Add(new LocDataItem());
        }
        EditorGUILayout.EndHorizontal();


        if (_localisedDataList.Count > 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key", GUILayout.Width(150));
            EditorGUILayout.LabelField("Value");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            foreach (var item in _localisedDataList)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.BeginHorizontal();
                item.Key = EditorGUILayout.TextArea(item.Key, GUILayout.Width(150)).ToUpper();
                item.Value = EditorGUILayout.TextArea(item.Value);
                EditorGUILayout.EndHorizontal();

                // draw remove button, that if clicked remove current from the list
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _localisedDataList.Remove(item);
                    break;
                }
                // Tell unity we no longer want the horizontal layout.
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        EditorGUILayout.EndVertical();

        if (refreshAssetDatabase)
        {
            //AssetDatabase.Refresh();
            refreshAssetDatabase = false;
        }
    }

    private void SaveChangesToFile()
    {
        DataDictionary cd = new DataDictionary();
        for (int i = 0; i < _localisedDataList.Count; i++)
        {
            cd.AddData(_localisedDataList[i].Key, _localisedDataList[i].Value);
        }
        string jsonData = JsonConvert.SerializeObject(cd.DataStorage, Formatting.Indented);
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Alert", "The filename is empty. Please fill them too !!", "Okay");
            return;
        }
        SaveData(fileName.ToLower(), jsonData);
    }


    public void SaveData(string fileName, string jsonData)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Alert", "Please enter the filename to save the data", "Okay");
            Debug.LogError("No file name entered");
            return;
        }

        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }


        if (_localisedDataList.Count == 0)
        {
            EditorUtility.DisplayDialog("Alert", "You need to add atleast one entry to save the file.", "Okay");
            return;
        }

        if (File.Exists(GetFilePath(fileName)))
        {
            if (EditorUtility.DisplayDialog("Warning", string.Format("{0} already exist. Are you sure you want to overwrite your work?", fileName), "Overwrite", "Cancel"))
            {
                File.WriteAllText(GetFilePath(fileName), jsonData);
            }
        }
        else
        {
            File.WriteAllText(GetFilePath(fileName), jsonData);
            AssetDatabase.Refresh();
        }
    }


    public string LoadDataEditor(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Alert", "Please enter the filename to load the data", "Okay");
            return null;
        }

        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }

        if (!File.Exists(GetFilePath(fileName)))
        {
            EditorUtility.DisplayDialog("File not found.", "Tip : Ensure you have added .json as an entension.", "Okay");
            return null;
        }
        return System.IO.File.ReadAllText(GetFilePath(fileName));
    }


    public string GetFilePath(string filename)
    {
        return Path.Combine(DataPath, filename);
    }

    private string DataPath
    {
        get
        {
            return Application.dataPath + "/Localisation/Translation/Resources/Languages";
        }
    }
}
#endif