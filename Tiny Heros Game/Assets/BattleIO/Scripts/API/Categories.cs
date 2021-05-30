using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class Category
{
    public int id;
    public string title;
    public int sort;
    public int status;
}

public class Categories
{
    public Category[] categories;
}