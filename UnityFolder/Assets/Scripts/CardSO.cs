using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewBonusCard", menuName = "ScriptableObjects/Bonus", order = 1)]
public class CardSO : ScriptableObject
{
    [SerializeField]
    public int iD;

    [SerializeField]
    public string cardName;

    [SerializeField]
    public string subtitle;

    [SerializeField]
    public string description;

    [SerializeField]
    public bool isEffectImmediate;

    [SerializeField]
    public int value;

    [SerializeField]
    public CardBonusEffect bonusEffect;

    [SerializeField]
    public Sprite cardSprite;
}

public struct CardInfo
{
    public int iD;
    public string cardName;
    public string subtitle;
    public string description;
    public bool isEffectImmediate;
    public int value;
    public CardBonusEffect bonusEffect;
    public Sprite cardSprite;

    public CardInfo(int ID, string CardName, string Subtitle, string Description, bool IsEffectImmediate, int Value, CardBonusEffect BonusEffect, Sprite sprite)
    {
        iD = ID;
        cardName = CardName;
        subtitle = Subtitle;
        description = Description;
        isEffectImmediate = IsEffectImmediate;
        value = Value;
        bonusEffect = BonusEffect;
        cardSprite = sprite;
    }
}
