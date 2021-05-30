using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CustomTweener : MonoBehaviour
{

    [System.Serializable]
    public class TweenObjects
    {
        public RectTransform target;
        public float time;
        public float startDelay;
        public Vector3 from;
        public Vector3 to;
        public LeanTweenType ltType;
    }

    public List<TweenObjects> scaleList;
    public List<TweenObjects> bounceList;

    void OnEnable()
    {
        AnimateScaleObjects();
        AnimatePositionObjects();
    }

    void OnDisable()
    {
        DisableEverything();
    }

    void AnimateScaleObjects()
    {
        foreach (var item in scaleList)
        {
            if (item == null)
                continue;
            if (item.target == null)
                continue;

            LTDescr ltDesc = LeanTween.scale(item.target, Vector3.one, item.time);
            ltDesc.setFrom(Vector3.one * 1.2f).setLoopPingPong().setDelay(item.startDelay);
            ltDesc.setEase(item.ltType);
        }
    }


    void AnimatePositionObjects()
    {
        foreach (var item in bounceList)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            //float yy = item.target.anchoredPosition.y;
       
            LTDescr ltDesc = LeanTween.move(item.target, item.from, item.time);
            ltDesc.setDelay(item.startDelay).setFrom(item.to);
            ltDesc.setLoopPingPong().setEase(item.ltType);

        }
    }

    private void DisableEverything()
    {
        foreach (var item in scaleList)
        {
            LeanTween.cancel(item.target);
        }

        foreach (var item in bounceList)
        {
            LeanTween.cancel(item.target);
            //Setting the anchored posoition to TO vector. 
            item.target.anchoredPosition = item.to;
        }
    }

    [ContextMenu("Set From")]
    void Evaluate()
    {
        foreach (var item in scaleList)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            item.to = Vector3.one;
            item.from = Vector3.one * 1.2f;
        }

        foreach (var item in bounceList)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            item.to = item.target.anchoredPosition;
            item.from = item.target.anchoredPosition + new Vector2(0, 25f);
        }
    }
}
