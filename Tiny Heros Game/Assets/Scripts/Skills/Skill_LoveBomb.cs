using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_LoveBomb : MonoBehaviour {

    public GameObject explosion;
    public SkillManager skill;

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag == "Enemy" && !collision.GetComponent<Enemy>().die)
        //{
        //    collision.GetComponent<Enemy>().TakeDamage(skill.Value);
        //    DestroyProjectile();
        //}
    }
}
