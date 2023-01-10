using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    [SerializeField]
    CardSO[] bonusCards;

    [SerializeField]
    GameObject cardPrefab;

    private void Start()
    {
        AddCardToDeck();
    }

    private void OnEnable()
    {
        PickCardFromDeck();
    }

    public void PickCardFromDeck()
    {
        int index = Random.Range(0, bonusCards.Length);
        CardSO randomBonusCard = bonusCards[index];
        Debug.Log(index);
        Debug.Log(randomBonusCard.cardName);
        GameObject go = Instantiate(cardPrefab, transform);
        go.GetComponentInChildren<Card>()
          .Setup(new CardInfo(randomBonusCard.iD, randomBonusCard.cardName, randomBonusCard.subtitle, 
          randomBonusCard.description, randomBonusCard.isEffectImmediate, randomBonusCard.value, randomBonusCard.bonusEffect, randomBonusCard.cardSprite));
    }

    public void RemoveCardFromDeck()
    {

    }

    public void AddCardToDeck()
    {

    }

}
