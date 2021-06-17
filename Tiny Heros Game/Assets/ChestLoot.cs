using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpwnerAreas;

    [SerializeField]
    GameObject[] Rewards;

    [SerializeField]
    Animator anim;

    // Salah W Osraa
    public void Start()
    {
        StartOpenChest();
    }

    // Update is called once per frame
    void StartOpenChest()
    {
        anim.SetBool("Open", true);
    }

    void OpenChest()
    {

        foreach (var area in SpwnerAreas)
        {
            GameObject item = Instantiate(Rewards[Random.Range(0, Rewards.Length)]);
            item.transform.position = SpwnerAreas[Random.Range(0, SpwnerAreas.Length)].transform.position;
        }

          Destroy(gameObject);
    }


}
