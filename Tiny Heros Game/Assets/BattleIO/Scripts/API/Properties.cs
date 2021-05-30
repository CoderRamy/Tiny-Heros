using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Property
{
    public Property_row property_row;
    public Media[] media;
}

[System.Serializable]
public class Property_row
{
    public int id;
    public string title;
    public string address;
    public string main_image;
    public double longitude;
    public double latitude;
    public float price;
    public string price_unit;
    public int cat_id;
    public int size;
    public string cat_title;
    public string desc;
}

[System.Serializable]
public class Media
{
    public string type;
    public string url;
}


[System.Serializable]
public class Properties
{
    public Property[] properties;
    public Media[] media;
}

public enum MediaType
{
    Image,
    Video,
}