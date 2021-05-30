using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenBase : MonoBehaviour
{
    [System.Serializable]
    public class TweenData
    {
        public RectTransform target;
        public float time;
        public float startDelay;
        public Vector3 from;
        public Vector3 to;
        public LeanTweenType ltType;
    }

    public List<TweenData> tweens;
    public bool playOnStart = false;

    void OnEnable()
    {
        AnimateObjects();
    }

    void OnDisable()
    {
        DisableEverything();
    }

    public virtual void AnimateObjects()
    {

    }

    public virtual void PlayAnimation()
    {

    }

    public virtual void ResetAnimation()
    {
        
    }

    private void DisableEverything()
    {
        foreach (var item in tweens)
        {
            if (item == null)
                continue;

            if (item.target == null)
                continue;
            LeanTween.cancel(item.target);
        }
    }


}
