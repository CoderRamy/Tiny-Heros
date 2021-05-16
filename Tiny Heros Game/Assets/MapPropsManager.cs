using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPropsManager : MonoBehaviour
{

    [System.Serializable]
    public struct Props
    {
        public GameObject _Props;
        SimpleSphereData SpawnZone;
        public int Count;
    }

  
    public Props[] _Props;

    // Start is called before the first frame update
    public void UpdateProps()
    {
        for(int i = 0; i < _Props.Length; i++){
            Debug.Log("acsses to " + i);
            for (int x = 0; x < _Props[i].Count; x++)
            {
                Debug.Log("loop to " + x);
                //Debug.Log("count to " + _Props[i].Count);
                GameObject _prop = Instantiate(_Props[i]._Props);
              //  _prop.transform.position = SpawnZone.GetRandomPosition();
            }
        }
    }
}
