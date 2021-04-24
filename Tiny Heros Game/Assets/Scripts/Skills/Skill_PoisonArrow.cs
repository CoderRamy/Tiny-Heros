using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Skill_PoisonArrow : MonoBehaviour
{
    public SkillManager skill;
    public float lifeTime = 200f;
    public float effectSpeed = 20f;
    private float CurrenteffectSpeed;

    private void Start()
    {
        CurrenteffectSpeed = effectSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        CurrenteffectSpeed -= Time.deltaTime;

        if (lifeTime >= 0)
        {
            if(CurrenteffectSpeed <= 0)
            {
                //if(!GetComponentInParent<Enemy>().die)
                //{
                //    GetComponentInParent<Enemy>().curentHelth -= skill.Value;
                //    CurrenteffectSpeed = effectSpeed;
                //}
            }
        }
        else
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
