using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterShards : MonoBehaviour
{

    [SerializeField]
    Transform CardHolder;

    [Header("Card Types")]
    [SerializeField]
    GameObject Slot_Normal;
    [SerializeField]
    GameObject Slot_Magic;
    [SerializeField]
    GameObject Slot_Rare;
    [SerializeField]
    GameObject Slot_Unique;

    GameObject CardType;

    // Start is called before the first frame update
    void Start()
    {
        
        foreach (var Character in GameInstance.AvailableCharacters){

            switch (Character.charactersRank)
            {
                case CharactersRank.Normal:
                    CardType = Slot_Normal;
                    break;
                case CharactersRank.Magic:
                    CardType = Slot_Magic;
                    break;
                case CharactersRank.Rare:
                    CardType = Slot_Rare;
                    break;
                case CharactersRank.Unique:
                    CardType = Slot_Unique;
                    break;
            }

            GameObject Card = Instantiate(CardType, CardHolder.transform);
            Card.transform.GetChild(0).GetComponent<TMP_Text>().text = Character.title;
            Card.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = Character.MaxCards.ToString();
            Card.transform.GetChild(1).GetChild(1).GetComponent<RawImage>().texture = Character.previewTexture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
