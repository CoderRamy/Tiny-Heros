using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HealUp : MonoBehaviour {


    public SkillManager skill;
    GameObject Player;
   // CharacterUnit playerInfo;

    // Use this for initialization
    public void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
      //  playerInfo = Player.GetComponent<CharacterUnit>();
        Invoke("DestroyProjectile", 2);
	}


    private void FixedUpdate()
    {
        //if (playerInfo.GetComponent<CharacterUnit>().health + skill.Value < playerInfo.maxHealth)
        //{
        //    playerInfo.health += skill.Value;
        //    ArenaUI.instance.ChangeHealth(playerInfo.health);
        //}
        //else if (playerInfo.health + skill.Value > playerInfo.maxHealth)
        //{
        //    playerInfo.health = playerInfo.maxHealth;
        //    ArenaUI.instance.ChangeHealth(playerInfo.health);
        //}
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


}
