using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_AnimationOnSelection : MonoBehaviour
{
    public bool isLeft;
    
    AnimationCurve scaleUpCurve;
    AnimationCurve movementCurve;
    float animationTime = 0.5f;
    float movementFactor;
    float scaleFactor;
    public RectTransform rectTransform;
    public RectTransform parentRectTransform;
    bool isAnimatingMovement;
    bool isAnimatingScale;
    Vector2 startPos;
    Quaternion startRot;
    Vector2 targetPos;
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
        rectTransform = transform.GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        // need to set an other way to display hand full of card instead of using horizontal layout
        startPos = parentRectTransform.anchoredPosition;
        startRot = parentRectTransform.rotation;
        StartCoroutine(AnimateMovement());
        StartCoroutine(AnimateScaleUp());
    }

    // need to be screen-size related instead of hand-size related
    void SetTargetPosition()
    {
        if (isLeft)
            targetPos = new Vector2(parentRectTransform.rect.width / 2 * 0.6f, parentRectTransform.rect.height * 1.25f);
        else
            targetPos = new Vector2(-parentRectTransform.rect.width / 2 * 0.6f, parentRectTransform.rect.height * 1.25f);
    }

    private void OnDisable()
    {
        StopCoroutine(AnimateMovement());
        StopCoroutine(AnimateScaleUp());
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
                SetTargetPosition();
                startPos = parentRectTransform.anchoredPosition;
                startRot = parentRectTransform.rotation;
                float elapsedTime = 0f;
                isAnimatingMovement = true;
                while (isAnimatingMovement)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > animationTime) isAnimatingMovement = false;
                    else
                    {
                        rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor);
                        parentRectTransform.rotation = Quaternion.Lerp(startRot, Quaternion.identity, movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor);
                    }
                    yield return new WaitForEndOfFrame();
                }
                rectTransform.anchoredPosition = targetPos;
                toggleOnMovement = false;
            }
            // not sure about this one
            else if (toggleOffMovement && !isAnimatingMovement)
            {
                float elapsedTime = 0f;
                isAnimatingMovement = true;
                while (isAnimatingMovement)
                {
                    elapsedTime += Time.deltaTime * 3;
                    if (elapsedTime > animationTime) isAnimatingMovement = false;
                    else
                    {
                        rectTransform.anchoredPosition = Vector2.Lerp(targetPos, startPos, elapsedTime / animationTime);
                        parentRectTransform.rotation = Quaternion.Lerp(Quaternion.identity, startRot, movementCurve.Evaluate(elapsedTime / animationTime) * movementFactor);
                    }
                    yield return new WaitForEndOfFrame();
                }
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
                        rectTransform.localScale = Vector3.one + scaleFactor * scaleUpCurve.Evaluate(elapsedTime / animationTime) * Vector3.one;
                    }
                    yield return new WaitForEndOfFrame();
                }
                toggleOnScale = false;
            }
            // not sure about this one
            else if (toggleOffScale && !isAnimatingScale)
            {
                float elapsedTime = 0f;
                isAnimatingScale = true;
                while (isAnimatingScale)
                {
                    elapsedTime += Time.deltaTime * 3;
                    if (elapsedTime > animationTime) isAnimatingScale = false;
                    else
                    {
                        rectTransform.localScale = Vector3.Lerp(Vector3.one * (scaleFactor + 1), Vector3.one, scaleUpCurve.Evaluate(elapsedTime / animationTime) * scaleFactor);
                    }
                    yield return new WaitForEndOfFrame();
                }
                rectTransform.localScale = Vector3.one;
                toggleOffScale = false;
                //yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}
