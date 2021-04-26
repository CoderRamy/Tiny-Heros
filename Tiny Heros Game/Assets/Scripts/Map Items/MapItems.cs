using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItems : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player hit the item");
            MapRandomItems[] mapRandomItems = GameObject.Find("RandomObjects").GetComponents<MapRandomItems>();

            foreach(var ItemScript in mapRandomItems)
            {
                if (ItemScript.MapItem.tag == gameObject.tag)
                {
                    Debug.Log("Destroy the item 1" + ItemScript.MapItem.name+" / "+ gameObject.name);
                    ItemScript.Current_itemCount--;
                    Destroy(this.gameObject);
                }
            }
        }
    }

}

