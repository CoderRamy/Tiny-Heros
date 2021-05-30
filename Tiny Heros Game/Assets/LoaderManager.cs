using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderManager : MonoBehaviour
{
    public static LoaderManager instance;

    public GameObject LoaderPanel;
    public bool IsLoading = false;
     
    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLoading)
        {
            LoaderPanel.SetActive(true);
        }
        else
        {
            LoaderPanel.SetActive(false);
        }
    }
}
