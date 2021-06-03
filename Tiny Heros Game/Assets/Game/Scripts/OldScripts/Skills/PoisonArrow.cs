using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : MonoBehaviour
{
    [SerializeField]
    GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy" && !collision.GetComponent<Enemy>().die)
        //{
        //    GameObject PoisonEffect = Instantiate(effect, collision.transform.position, collision.transform.rotation);
        //    PoisonEffect.transform.SetParent(collision.transform);
        //}
    }
}
