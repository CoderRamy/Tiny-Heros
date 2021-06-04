using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesDBLoader : MonoBehaviour
{
    public Action OnDataLoad;

    public static CategoriesDBLoader instance;

    DataLoader dataloader = new DataLoader();
    public Categories categories = new Categories();

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


    public IEnumerator GetData()
    {
        CoroutineWithData cd = new CoroutineWithData(this, dataloader.LoadCategories());
        yield return cd.coroutine;
        categories = cd.result as Categories;
        LoaderManager.instance.IsLoading = false;
        OnDataLoad();
    }


}
