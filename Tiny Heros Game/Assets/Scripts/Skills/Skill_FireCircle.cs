using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireCircle : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public SkillManager skill;

    // Use this for initialization
    public  void Start () {

      //  base.Start();
        Invoke("DestroyProjectile", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
       // transform.Translate(GameObject.FindGameObjectWithTag("Player").GetComponent<Character4D>().Direction * speed * Time.deltaTime);
    }



    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag == "Enemy" && !collision.GetComponent<Enemy>().die)
        //{
        //   // DestroyProjectile();
        //    collision.GetComponent<Enemy>().TakeDamage(skill.Value);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            DestroyProjectile();
        }
    }

}
