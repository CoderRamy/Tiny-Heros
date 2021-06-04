using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRofe : MonoBehaviour
{

    public GameObject roof;
    public float fadeSpeed;

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeOut());
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(FadeIn());
        }
    }

    public IEnumerator FadeIn()
    {
        while(roof.GetComponent<MeshRenderer>().material.color.a < 1)
        {
            Color ObjectColor = roof.GetComponent<MeshRenderer>().material.color;
            float fadeAmount = ObjectColor.a + (fadeSpeed * Time.deltaTime);
            ObjectColor = new Color(ObjectColor.r, ObjectColor.g, ObjectColor.b, fadeAmount);
            roof.GetComponent<MeshRenderer>().material.color = ObjectColor;
           yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (roof.GetComponent<MeshRenderer>().material.color.a > 0)
        {
            Color ObjectColor = roof.GetComponent<MeshRenderer>().material.color;
            float fadeAmount = ObjectColor.a - (fadeSpeed * Time.deltaTime);
            ObjectColor = new Color(ObjectColor.r, ObjectColor.g, ObjectColor.b, fadeAmount);
            roof.GetComponent<MeshRenderer>().material.color = ObjectColor;
            yield return null;
        }
    }

}
