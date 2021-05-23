using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelect : MonoBehaviour
{

    public GameObject CharacterHolder;
    public GameObject CharacterSelectorUI;
    public GameObject MenuUI;

    public void OpenSelector()
    {
        CharacterSelectorUI.SetActive(true);
        MenuUI.SetActive(false);
        CharacterHolder.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
    }
    public void CloseSelector()
    {
        CharacterSelectorUI.SetActive(false);
        MenuUI.SetActive(true);
        CharacterHolder.transform.localScale = new Vector3(1, 1, 1);
    }

}
