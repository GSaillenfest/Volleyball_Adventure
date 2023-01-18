using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOnSelectionAnimation : MonoBehaviour
{
    AnimationCurve scaleUpCurve;
    AnimationCurve movementCurve;
    float animationTime = 0.5f;
    float movementFactor;
    float scaleFactor;
    public RectTransform parentRectTransform;
    bool isAnimatingMovement;
    bool isAnimatingScale;
    Vector2 startPos;
    Vector2 targetPos = new Vector2(750, 650);
    bool toggleOnMovement;
    bool toggleOnScale;
    bool toggleOffMovement;
    bool toggleOffScale;

    UIActionDisplay uiActionDisplay;

    private void Awake()
    { // setter to call for an update every time I change a parameter in the animator ?
        uiActionDisplay = FindObjectOfType<UIActionDisplay>();
        scaleUpCurve = uiActionDisplay.cardAnimator.scaleUpCurve;
        movementCurve = uiActionDisplay.cardAnimator.movementCurve;
        animationTime = uiActionDisplay.cardAnimator.animationTime;
        movementFactor = uiActionDisplay.cardAnimator.movementFactor;
        scaleFactor = uiActionDisplay.cardAnimator.scaleFactor;
    }
    private void OnEnable()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        StartCoroutine(AnimateMovement());
        StartCoroutine(AnimateScaleUp());
    }

    private void OnDisable()
    {
        StopCoroutine(AnimateMovement());
        StopCoroutine(AnimateScaleUp());
    }

    // start coroutine when clicked on
    public void AnimateOnSelection(bool isSelected)
    {
        Debug.Log("animate");
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
                startPos = parentRectTransform.localPosition;
                float elapsedTime = 0f;
                isAnimatingMovement = true;
                while (isAnimatingMovement)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > animationTime) isAnimatingMovement = false;
                    else
                    {
                        parentRectTransform.localPosition = Vector2.Lerp(startPos, targetPos, movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor);
                    }
                    yield return new WaitForEndOfFrame();
                }
                toggleOnMovement = false;
            }
            // not sure about this one
            else if (toggleOffMovement && !isAnimatingMovement)
            {
                float elapsedTime = 0f;
                Debug.Log("here");
                isAnimatingMovement = true;
                while (isAnimatingMovement)
                {
                    elapsedTime += Time.deltaTime * 3;
                    if (elapsedTime > animationTime) isAnimatingMovement = false;
                    else
                    {
                        parentRectTransform.localPosition = Vector2.Lerp(targetPos, startPos, elapsedTime / animationTime);
                    }
                    yield return new WaitForEndOfFrame();
                }
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
                        parentRectTransform.localScale = Vector3.one + Vector3.one * scaleUpCurve.Evaluate(elapsedTime / animationTime) * scaleFactor;
                    }
                    yield return new WaitForEndOfFrame();
                }
                toggleOnScale = false;
            }
            // not sure about this one
            else if (toggleOffScale && !isAnimatingScale)
            {
                float elapsedTime = 0f;
                Debug.Log("here2");
                isAnimatingScale = true;
                while (isAnimatingScale)
                {
                    elapsedTime += Time.deltaTime * 3;
                    if (elapsedTime > animationTime) isAnimatingScale = false;
                    else
                    {
                        parentRectTransform.localScale = Vector3.Lerp(Vector3.one * (scaleFactor+1), Vector3.one, scaleUpCurve.Evaluate(elapsedTime / animationTime) * scaleFactor);
                    }
                    yield return new WaitForEndOfFrame();
                }
                parentRectTransform.localScale = Vector3.one;
                toggleOffScale = false;
                //yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}
