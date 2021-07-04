using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour {
    
    public SkillManager skill;

    Image icon, fill;
    Button btn;
    private float cooldownTimer = 0;
    GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");

        icon = transform.GetChild(0).GetComponent<Image>();
        icon.sprite = skill.icon;

        fill = transform.GetChild(1).GetComponent<Image>();

        btn = GetComponent<Button>();
        btn.onClick.AddListener(useSkill);
    }

    void useSkill()
    {
        //if (!CharacterUnit.instance.die)
        //{
        //    if (skill.enemyRequired)
        //    {
        //        if (Player.GetComponent<BattleRoles>().selectedTarget == null)
        //        {
        //            return;
        //        }
        //    }

        //    if (Time.time >= cooldownTimer)
        //    {
        //        skill.Use(skill.name, Player.GetComponent<CharacterUnit>());
        //        cooldownTimer = Time.time + skill.cooldown;
        //    }
        //}

    }

    private void Update()
    {
        if (Time.time <= cooldownTimer)
        {
            fill.fillAmount += (1 / skill.cooldown) * Time.deltaTime;
        }
        else
        {
            fill.fillAmount = 0;
        }
    }

}
