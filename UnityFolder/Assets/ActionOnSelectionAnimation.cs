using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnSelectionAnimation : MonoBehaviour
{
    AnimationCurve scaleUpCurve;
    AnimationCurve movementCurve;
    float animationTime = 0.5f;
    float movementFactor;
    public RectTransform rectTransform;
    bool isAnimating;
    Vector2 startPos;
    bool toggleOn;
    bool toggleOff;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        StartCoroutine(AnimateMovement());
        //StartCoroutine(ToggleOff());
    }


    // start coroutine when clicked on
    public void AnimateOnSelection(bool isSelected)
    {
        if (isSelected)
            toggleOn = true;
        else
            toggleOff = true;
    }

    private IEnumerator AnimateMovement()
    {
        while (true)
        {
            if (toggleOn && !isAnimating)
            {
                float elapsedTime = 0f;
                Debug.Log("here");
                isAnimating = true;
                while (isAnimating)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > animationTime) isAnimating = false;
                    else
                    {
                        rectTransform.anchoredPosition = startPos + Vector2.up * movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor;
                    }
                    yield return new WaitForEndOfFrame();
                }
                rectTransform.anchoredPosition = startPos;
                toggleOn = false;
            }
            else if (toggleOff && !isAnimating)
            {
                float elapsedTime = 0f;
                Debug.Log("here");
                isAnimating = true;
                while (isAnimating)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime*2 > animationTime) isAnimating = false;
                    else
                    {
                        rectTransform.anchoredPosition = startPos + Vector2.up * movementCurve.Evaluate(1 - (elapsedTime*2 / animationTime)) * movementFactor;
                    }
                    yield return new WaitForEndOfFrame();
                }
                rectTransform.anchoredPosition = startPos;
                toggleOff = false;
            }
            yield return null;
        }
    }

    private IEnumerator ToggleOff()
    {
        yield return new WaitForEndOfFrame();
    }
}
