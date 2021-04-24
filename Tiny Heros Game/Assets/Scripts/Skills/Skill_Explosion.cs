using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Skill_Explosion : MonoBehaviour
{
    public SkillManager skill;
    public float lifeTime = 4f;
    public LayerMask Enemy;
    public float attackRange = 5f;

    public void Start()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, Enemy);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
          //  enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(skill.Value);
        }

        Invoke("DestroyProjectile", lifeTime);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
