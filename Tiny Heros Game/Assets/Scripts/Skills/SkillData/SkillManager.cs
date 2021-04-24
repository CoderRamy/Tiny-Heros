using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Matchstick War/Skill", order = 4)]
public class SkillManager : ScriptableObject
{

    //53 - 85 - 104 - 106 - 108 - 112 - 113 - 119 - 120 

    //151 to 161 weather

    new public string name = "New Skill";
    public Sprite icon = null;
    public float cooldown;
    public float Value;
    public GameObject effect;
    public bool enemyRequired;


    [HideInInspector]
    public int listIdx = 0;
    [HideInInspector]
    public List<string> skillTypes = new List<string>(new string[] { "HealUp", "FireCircle", "Explosion" });



    //public virtual void Use(string skillType, CharacterUnit player)
    //{
       
    //    switch (skillType)
    //    {
    //        case "HealUp":
    //            GameObject HeallEffect = Instantiate(effect, new Vector3(player.transform.position.x, player.transform.position.y, -1), player.transform.rotation);
    //            HeallEffect.transform.SetParent(player.transform);
    //            break;
    //        case "FireCircle":
    //            Instantiate(effect, player.transform.GetChild(1).position, player.transform.rotation);
    //            break;
    //        case "Explosion":
    //            Instantiate(effect, player.transform.position, player.transform.rotation);
    //            break;
    //        case "LoveBomb":
    //            Instantiate(effect, player.transform.position, player.transform.rotation);
    //            break;
    //        case "MoveSpeed":
    //             GameObject SpeedEffect = Instantiate(effect, new Vector3(player.transform.position.x, player.transform.position.y, -1)   , player.transform.rotation);
    //             SpeedEffect.transform.SetParent(player.transform);
    //            break;  
    //        case "PoisonArrow":
    //             Instantiate(effect, player.transform.position, player.transform.rotation);
    //            break;
    //    }

    //    // effect.GetComponent<ParticleSystem>().Play();
       
    //}
}
