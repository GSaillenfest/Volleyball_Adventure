using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour
{
    [SerializeField]
    BonusVPlayer[] bonusCards;

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
        //Instantiate<>();
    }

    public void RemoveCardFromDeck()
    {

    }

    public void AddCardToDeck()
    {

    }

}
