using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestLoot : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpwnerAreas;

    [SerializeField]
    GameObject[] Rewards;

    Animator anim;

    [SerializeField]
    MeshRenderer[] meshRenderers;

    [SerializeField]
    float fadeSpeed;

    [SerializeField]
    [Tooltip("Dont insert number more than Spwner Areas length")]
    int MaxLuckyloot;

    int LuckylootNumber;

    [SerializeField]
    Image Loader;
    [SerializeField]
    float CurentTimer;

    [SerializeField]
    float Timer;

    [SerializeField]
    bool IsEquip;

    GameObject Character;

    // Salah W Osraa
    public void Start()
    {
        CurentTimer = Timer;
        Loader.fillAmount = 1;
    }

    public void Update()
    {
        if (IsEquip && CurentTimer > 0)
        {
            CurentTimer -= Time.deltaTime;
            Loader.fillAmount -= 1.0f / Timer * Time.deltaTime;
        }

        if (CurentTimer <= 0)
        {
           StartOpenChest();
        }
    }

    // Update is called once per frame
    public void StartOpenChest()
    {
        Debug.Log("test");
        anim = GetComponent<Animator>();
        anim.SetBool("Open", true);
    }

    void OpenChest()
    {

        LuckylootNumber = Random.Range(0, MaxLuckyloot);

        for(int i = -1; i < LuckylootNumber; i++)
        {
            GameObject item = Instantiate(Rewards[Random.Range(0, Rewards.Length)]);
            item.transform.position = SpwnerAreas[i+1].transform.position;
        }

        StartCoroutine(FadeOut());

    }


    public IEnumerator FadeOut()
    {

        while (meshRenderers[0].material.color.a > 0)
        {
            Color ObjectColor = meshRenderers[0].material.color;
            float fadeAmount = ObjectColor.a - (fadeSpeed * Time.deltaTime);
            ObjectColor = new Color(ObjectColor.r, ObjectColor.g, ObjectColor.b, fadeAmount);

            foreach (var mesh in meshRenderers)
            {
                mesh.material.color = ObjectColor;
                mesh.material.color = ObjectColor;
            }

            yield return null;
        }

         Destroy(gameObject);

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            IsEquip = true;
            Character = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Character)
        {
            IsEquip = false;
            CurentTimer = Timer;
            Loader.fillAmount = 1;
        }
    }





}
