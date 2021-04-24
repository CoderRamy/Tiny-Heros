using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRace : MonoBehaviour
{
    Transform classHolder;
    public Transform classButtons;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(DoSwitch);

        classHolder = GameObject.Find("ClassHolder").transform;

        Transform[] getFirst = transform.parent.GetComponentsInChildren<Transform>();

        if (getFirst[0].GetChild(0) == transform)
        {
            DoSwitch();
        }
    }

    public void DoSwitch()
    {
        Transform Buttons = Instantiate(classButtons, classHolder);   
    }

}
