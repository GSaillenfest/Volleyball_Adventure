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
    float scaleFactor;
    public RectTransform rectTransform;
    bool isAnimatingMovement;
    bool isAnimatingScale;
    Vector2 startPos;
    bool toggleOnMovement;
    bool toggleOnScale;
    bool toggleOffMovement;
    bool toggleOffScale;

    UIActionDisplay uiActionDisplay;

    private void Awake()
    { // setter to call for an update every time I change a parameter in the animator ?
        uiActionDisplay = FindObjectOfType<UIActionDisplay>();
        scaleUpCurve = uiActionDisplay.actionAnimator.scaleUpCurve;
        movementCurve = uiActionDisplay.actionAnimator.movementCurve;
        animationTime = uiActionDisplay.actionAnimator.animationTime;
        movementFactor = uiActionDisplay.actionAnimator.movementFactor;
        scaleFactor = uiActionDisplay.actionAnimator.scaleFactor;
    }
    private void OnEnable()
    {
        StartCoroutine(AnimateMovement());
        StartCoroutine(AnimateScaleUp());
    }

    private void OnDisable()
    {
        StopCoroutine(AnimateMovement());
        StopCoroutine(AnimateScaleUp());
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }


    // start coroutine when clicked on
    public void AnimateOnSelection(bool isSelected)
    {
        if (isSelected)
        {
            toggleOnMovement = true;
            toggleOnScale = true;
        }
        else
        {
            toggleOffMovement = true;
            toggleOffScale = true;
        }
    }

    private IEnumerator AnimateMovement()
    {
        while (true)
        {
            if (toggleOnMovement && !isAnimatingMovement)
            {
                float elapsedTime = 0f;
                isAnimatingMovement = true;
                while (isAnimatingMovement)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > animationTime) isAnimatingMovement = false;
                    else
                    {
                        rectTransform.anchoredPosition = startPos + Vector2.up * movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor;
                    }
                    yield return new WaitForEndOfFrame();
                }
                rectTransform.anchoredPosition = startPos;
                toggleOnMovement = false;
            }
            // not sure about this one
            else if (toggleOffMovement && !isAnimatingMovement)
            {
                //float elapsedTime = 0f;
                //Debug.Log("here");
                //isAnimating = true;
                //while (isAnimating)
                //{
                //    elapsedTime += Time.deltaTime;
                //    if (elapsedTime*2 > animationTime) isAnimating = false;
                //    else
                //    {
                //        rectTransform.anchoredPosition = startPos + Vector2.up * movementCurve.Evaluate(1 - (elapsedTime*2 / animationTime)) * movementFactor;
                //    }
                //    yield return new WaitForEndOfFrame();
                //}
                rectTransform.anchoredPosition = startPos;
                toggleOffMovement = false;
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }

    private IEnumerator AnimateScaleUp()
    {
        while (true)
        {
            if (toggleOnScale && !isAnimatingScale)
            {
                float elapsedTime = 0f;
                isAnimatingScale = true;
                while (isAnimatingScale)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > animationTime) isAnimatingScale = false;
                    else
                    {
                        rectTransform.localScale = Vector3.one + Vector3.one * scaleUpCurve.Evaluate(elapsedTime / animationTime) * scaleFactor;
                    }
                    yield return new WaitForEndOfFrame();
                }
                toggleOnScale = false;
            }
            // not sure about this one
            else if (toggleOffScale && !isAnimatingScale)
            {
                //float elapsedTime = 0f;
                //Debug.Log("here");
                //isAnimating = true;
                //while (isAnimating)
                //{
                //    elapsedTime += Time.deltaTime;
                //    if (elapsedTime*2 > animationTime) isAnimating = false;
                //    else
                //    {
                //        rectTransform.anchoredPosition = startPos + Vector2.up * movementCurve.Evaluate(1 - (elapsedTime*2 / animationTime)) * movementFactor;
                //    }
                //    yield return new WaitForEndOfFrame();
                //}
                rectTransform.localScale = Vector3.one;
                toggleOffScale = false;
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}
