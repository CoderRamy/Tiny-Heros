using KamaliDebug;
using UnityEngine;

public class MapRandomItems : MonoBehaviour
{
    [SerializeField]
    public GameObject MapItem;

    [SerializeField]
    Transform RandomPointHolder;

    [SerializeField]
    float mapMinSize_X = 13.44f;

    [SerializeField]
    float mapMixSize_X = -13.44f;

    [SerializeField]
    float mapMinSize_Z = 13.37f;

    [SerializeField]
    float mapMixSize_Z = -13.37f;

    [SerializeField]
    float mapSize_Y = 0.651f;

    [SerializeField]
    float itemCount;

    public float Current_itemCount;

    [SerializeField]
    float dailyTimer = 1f; // how much time will path to create new item on map

    float currentTimer; // count down timer 

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = dailyTimer;

        for (int i = 0; i < itemCount; i++)
        {
            CreateMapItem();
        }
    }

    public void Update()
    {
        if(Current_itemCount < itemCount)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                CreateMapItem();
                currentTimer = dailyTimer;
            }
        }
    }


    void CreateMapItem()
    {
        var RandomX = Random.Range(mapMinSize_X, mapMixSize_X);
        var RandomZ = Random.Range(mapMinSize_Z, mapMixSize_Z);
        Vector3 randPosition = new Vector3(RandomX, mapSize_Y, RandomZ);
        GameObject _Item = Instantiate(MapItem, RandomPointHolder);
        _Item.transform.position = randPosition;
        Current_itemCount++;
    }


}
