using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "UIAnimator", menuName = "ScriptableObjects")]
public class UIAnimator : ScriptableObject
{
    [SerializeField]
    public AnimationCurve scaleUpCurve;

    [SerializeField]
    public AnimationCurve movementCurve;

    [SerializeField]
    public float animationTime = 0.5f;

    [SerializeField]
    [Range(0f, 100f)]
    public float movementFactor; 
    
    [SerializeField]
    [Range(0f, 1f)]
    public float scaleFactor;
}
