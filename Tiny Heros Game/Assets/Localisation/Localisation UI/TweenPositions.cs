using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPositions : TweenBase
{
    public override void AnimateObjects()
    {
        if (!playOnStart)
            return;

        PlayAnimation();
    }

    public override void PlayAnimation()
    {
        foreach (var item in tweens)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            Vector3 initPos = item.to;
            item.target.anchoredPosition = item.from;

            LTDescr ltDesc = LeanTween.move(item.target, initPos, item.time).setFrom(item.from);
            ltDesc.setDelay(item.startDelay).setEase(item.ltType);

        }
    }

    public override void ResetAnimation()
    {
        foreach (var item in tweens)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            item.target.anchoredPosition = item.from;

        }
    }

    [ContextMenu("Set To")]
    void Evaluate()
    {
        foreach (var item in tweens)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;

            item.to = item.target.anchoredPosition;
        }
    }
}
