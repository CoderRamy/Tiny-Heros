using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlineManager : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            GetComponent<WalkingOrc>().enabled = true;
        }
        else
        {
            GetComponent<WalkingOrc>().enabled = false;
        }

        DontDestroyOnLoad(gameObject);
    }

}
