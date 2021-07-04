using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(ChestLoot))]
public class ChestLootEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChestLoot chestLoot = (ChestLoot) target;

        if(GUILayout.Button("Open Chest"))
        {
            chestLoot.StartOpenChest();
        }
    }
}
