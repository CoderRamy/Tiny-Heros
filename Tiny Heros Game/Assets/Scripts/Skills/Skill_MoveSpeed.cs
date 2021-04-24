using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MoveSpeed : MonoBehaviour {


    public SkillManager skill;
    GameObject Player;
    // Use this for initialization
    public void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
       // Player.GetComponent<PlayerControllerOffline>().speed += skill.Value;
        Invoke("DestroyProjectile", 2);
	}
	
    void DestroyProjectile()
    {
       // Player.GetComponent<PlayerControllerOffline>().speed -= skill.Value;
        Destroy(gameObject);
    }


}
