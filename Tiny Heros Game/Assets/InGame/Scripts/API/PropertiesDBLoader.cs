using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesDBLoader : MonoBehaviour
{
    public Action OnDataLoad;

    public static PropertiesDBLoader instance;

    DataLoader dataloader = new DataLoader();
    public Properties properties = new Properties();

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update 
    void Start()
    {
        LoaderManager.instance.IsLoading = true;
        StartCoroutine(GetData());
       
    }


    public IEnumerator GetData(string cat_id = "all" , string family = "all")
    {
        CoroutineWithData cd = new CoroutineWithData(this, dataloader.LoadProperties(cat_id,family));
        yield return cd.coroutine;
        properties = cd.result as Properties;
        LoaderManager.instance.IsLoading = false;
        OnDataLoad();
    }


}
