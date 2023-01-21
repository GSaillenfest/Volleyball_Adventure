using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeckDisplay : MonoBehaviour
{
    Vector3 pivotPoint = new Vector3(0, -3000);
    float maxAngle;
    float minAngle;
    float cardCount;
    float handHeight;
    float radius;
    float handWidth;

    public void PositionCardInHand()
    {
        cardCount = transform.childCount;
        handWidth = transform.GetComponent<RectTransform>().rect.width - 271;
        handHeight = transform.GetComponent<RectTransform>().rect.height;
        radius = Mathf.Max(cardCount, 3) * handHeight;
        maxAngle = (180 / 3.14159f) * handWidth / radius;
        maxAngle = Mathf.Clamp(maxAngle, 0, 30);
        if (cardCount == 1) return;
        else minAngle = maxAngle / (cardCount-1);

        for (int i = 0; i < cardCount; i++)
        {
            pivotPoint =  new Vector2(0.5f, -radius/handHeight);
            transform.GetChild(i).GetComponent<RectTransform>().pivot = pivotPoint;
            float angle = minAngle * i - maxAngle/2;
            transform.GetChild(i).GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}
