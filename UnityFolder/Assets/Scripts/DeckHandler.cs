using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    [SerializeField]
    CardSO[] bonusCards;

    [SerializeField]
    GameObject cardPrefab;

    List<CardSO> deck = new();

    bool isfirstTurn = true;

    private void Start()
    {
    }

    private void OnEnable()
    {
        if (isfirstTurn)
        {
            isfirstTurn = false;
            AddCardToDeck();
        }
        PickCardFromDeck();
    }

    public void PickCardFromDeck()
    {
        if (deck.Count == 0) return;
        int randomIndex = Random.Range(0, deck.Count);
        CardSO randomBonusCard = deck[randomIndex];
        //Debug.Log(index);
        //Debug.Log(randomBonusCard.cardName);
        GameObject go = Instantiate(cardPrefab, transform);
        go.GetComponentInChildren<Card>()
          .Setup(new CardInfo(randomBonusCard.iD, randomBonusCard.cardName, randomBonusCard.subtitle,
          randomBonusCard.description, randomBonusCard.isEffectImmediate, randomBonusCard.value, randomBonusCard.bonusEffect, randomBonusCard.cardSprite));
        RemoveCardFromDeck(randomBonusCard);
    }

    public void RemoveCardFromDeck(CardSO cardToRemove)
    {
        deck.Remove(cardToRemove);
    }

    public void AddCardToDeck()
    {
        deck.AddRange(bonusCards);
    }

}
