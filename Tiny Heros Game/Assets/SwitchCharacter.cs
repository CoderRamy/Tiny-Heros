using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCharacter : MonoBehaviour
{
    public static SwitchCharacter instance;

    public GameObject UICharacter;
    public GameObject GameCharacter;
    Transform UICharacterHolder;
    AudioSource audioSource;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        UICharacterHolder = GameObject.Find("UI_Character_Holder").transform;
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(DoSwitch);
        audioSource = GetComponent<AudioSource>();

        Transform[] getFirst = transform.parent.GetComponentsInChildren<Transform>();

        Debug.Log(getFirst[0].GetChild(0));

        if(getFirst[0].GetChild(0) == transform)
        {
            DoSwitch();
        }

    }

    public void DoSwitch()
    {

        Transform roleHolder = transform.parent;

        foreach (Transform role in roleHolder)
        {
            role.GetChild(0).GetChild(0).gameObject.SetActive(false);
            role.GetChild(0).GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255,255);

            if(UICharacterHolder.transform.childCount > 0)
            {
                Destroy(UICharacterHolder.transform.GetChild(0).gameObject);
            }
            
        }

        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(1).GetComponent<Image>().color = new Color32(252, 228, 132, 255);
        GameObject character = Instantiate(UICharacter, UICharacterHolder);
        if (audioSource) { audioSource.Play(); }

        GameObject.Find("SelectedCharacter").GetComponent<SelectCharacter>().SelectedCharacter = GameCharacter;

    }
}
