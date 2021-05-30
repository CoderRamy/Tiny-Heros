using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScalers : TweenBase {

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

            //Vector3 initScale = item.to;
            //item.target.anchoredPosition = item.from;

            item.target.localScale = item.from;

            LTDescr ltDesc = LeanTween.scale(item.target, item.to, item.time).setFrom(item.from);
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

            item.target.localScale = item.from;

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

            item.to = Vector3.one;
        }
    }
}
