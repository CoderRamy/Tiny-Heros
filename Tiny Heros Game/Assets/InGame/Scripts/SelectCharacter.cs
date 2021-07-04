using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter inctance;

    public GameObject SelectedCharacter;

    public void Awake()
    {
            DontDestroyOnLoad(this);
    }



}
