using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualStateTweenPositions : MonoBehaviour
{
    public Item item;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        LTDescr ltDesc = LeanTween.move(item.target, item.mid, item.time).setFrom(item.start);
        ltDesc.setDelay(item.startDelay).setEase(item.easeType);
        ltDesc.setLoopType(item.loopType);
        ltDesc.setOnComplete(() => {
            //Debug.Log("Completed");

            State2Start();
        });
    }

    private void State2Start()
    {
        LTDescr ltDesc = LeanTween.move(item.target, item.end, item.time).setFrom(item.mid);
        ltDesc.setDelay(item.startDelay).setEase(item.easeType);
        ltDesc.setLoopType(item.loopType);
        ltDesc.setOnComplete(() => {
            //Debug.Log("Completed");

            State3Start();
        });
    }

    private void State3Start()
    {

        LTDescr ltDesc = LeanTween.move(item.target, item.mid, item.time).setFrom(item.end);
        ltDesc.setDelay(item.startDelay).setEase(item.easeType);
        ltDesc.setLoopType(item.loopType);
        ltDesc.setOnComplete(() => {
            //Debug.Log("Completed");

            State4Start();
        });
    }

    private void State4Start()
    {
        LTDescr ltDesc = LeanTween.move(item.target, item.start, item.time).setFrom(item.mid);
        ltDesc.setDelay(item.startDelay).setEase(item.easeType);
        ltDesc.setLoopType(item.loopType);
        ltDesc.setOnComplete(() => {
            //Debug.Log("Completed");

            Init();
        });
    }

}

[System.Serializable]
public class Item
{
    public Vector2 start;
    public Vector2 mid;
    public Vector3 end;

    public float time = 1f;
    public float startDelay;

    public LeanTweenType easeType;
    public LeanTweenType loopType = LeanTweenType.once;

    public RectTransform target;
}