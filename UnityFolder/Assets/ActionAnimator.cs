using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimator : ScriptableObject
{
    [SerializeField]
    static public AnimationCurve scaleUpCurve;

    [SerializeField]
    static public AnimationCurve movementCurve;
    
    [SerializeField]
    static public float animationTime = 0.5f;

    [SerializeField]
    static public float movementFactor;

    public RectTransform rectTransform;
}
