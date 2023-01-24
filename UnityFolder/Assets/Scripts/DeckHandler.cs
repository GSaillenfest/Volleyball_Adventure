using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    [SerializeField]
    CardSO[] bonusCards;

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    bool left;

    List<CardSO> deck = new();
    bool isfirstTurn = true;
    float scale = 1;

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
        DisplayCard();
    }

    private void DisplayCard()
    {
        GetComponent<UIDeckDisplay>().PositionCardInHand();
    }

    public void PickCardFromDeck()
    {
        if (deck.Count == 0) return;
        int randomIndex = Random.Range(0, deck.Count);
        CardSO randomBonusCard = deck[randomIndex];
        //Debug.Log(index);
        //Debug.Log(randomBonusCard.cardName);
        GameObject go = Instantiate(cardPrefab, transform);
        go.GetComponent<RectTransform>().localScale =
            new Vector3(
                Mathf.Clamp(scale, 0.5f, 1f),
                Mathf.Clamp(scale, 0.5f, 1f)
                );
        go.GetComponentInChildren<Card>()
          .Setup(new CardInfo(randomBonusCard.iD, randomBonusCard.cardName, randomBonusCard.subtitle,
          randomBonusCard.description, randomBonusCard.isEffectImmediate, randomBonusCard.value, randomBonusCard.bonusEffect, randomBonusCard.cardSprite));
        go.GetComponentInChildren<Card_AnimationOnSelection>().isLeft = left;
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
