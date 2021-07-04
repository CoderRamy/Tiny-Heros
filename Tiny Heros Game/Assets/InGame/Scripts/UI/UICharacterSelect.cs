using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterSelect : MonoBehaviour
{
    public static UICharacterSelect instance;
    public GameObject CharacterHolder;
    public GameObject CharacterSelectorUI;
    public GameObject MenuUI;

    [Header("Character Data")]
    public TMP_Text TXTName;
    public TMP_Text TXTPrice;
    public TMP_Text TXTDesc;
    [Header("Character States")]
    public TMP_Text TXTHP;
    public TMP_Text TXTDamage;
    public TMP_Text TXTDefend;
    public TMP_Text TXTMoveSpeed;
    public TMP_Text TXTExpRate;
    public TMP_Text TXTGoldRate;
    public TMP_Text TXTHpRecovery;
    public TMP_Text TXTDamageRateLeechHP;
    public Image Icon;

    public CharacterData characterData;


    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        //Icon.sprite = characterData.iconTexture;
        TXTName.text = characterData.title;
        TXTDesc.text = characterData.description;
        TXTHP.text = characterData.stats.addHp.ToString();
        TXTPrice.text = characterData.price.amount.ToString();
        TXTDamage.text = characterData.stats.addAttack.ToString();
        TXTDefend.text = characterData.stats.addDefend.ToString();
        TXTMoveSpeed.text = characterData.stats.addMoveSpeed.ToString();
        TXTExpRate.text = characterData.stats.addExpRate.ToString();
        TXTGoldRate.text = characterData.stats.addScoreRate.ToString();
        TXTHpRecovery.text = characterData.stats.addHpRecoveryRate.ToString();
        TXTDamageRateLeechHP.text = characterData.stats.addDamageRateLeechHp.ToString();
    }

    public void OpenSelector()
    {
        CharacterSelectorUI.SetActive(true);
        MenuUI.SetActive(false);
        CharacterHolder.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
        CharacterHolder.transform.position = new Vector3(0, -0.07f, 89.9f);
    }
    public void CloseSelector()
    {
        CharacterSelectorUI.SetActive(false);
        MenuUI.SetActive(true);
        CharacterHolder.transform.localScale = new Vector3(1, 1, 1);
        CharacterHolder.transform.position = new Vector3(0, 0, 89.9f);
    }

}
