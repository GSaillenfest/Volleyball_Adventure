using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Card class is a component of the Card Prefab
public class Card : ActionBehaviour
{
    #region Parameters

    [SerializeField]
    TMP_Text cardNameField; 
    
    [SerializeField]
    TMP_Text subtitleField;   
    
    [SerializeField]
    TMP_Text descriptionField;

    [SerializeField]
    Image imageField;

    int iD;
    string cardName;
    string subtitle;
    string description;
    bool isEffectImmediate;
    int value;
    CardBonusEffect bonusEffect;

    [SerializeField]
    EffectManager effectManager;

    CardInfo _cardInfo;

    #endregion

    public override void OnEnable()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnBonusCardSelection += CheckForSelected;
        effectManager = FindObjectOfType<EffectManager>();
        base.OnEnable();
    }

    public void Setup(CardInfo cardInfo)
    {
        iD = cardInfo.iD;
        cardNameField.SetText(cardInfo.cardName);
        subtitleField.SetText(cardInfo.subtitle);
        descriptionField.SetText(cardInfo.description);
        isEffectImmediate = cardInfo.isEffectImmediate;
        value = cardInfo.value;
        bonusEffect = cardInfo.bonusEffect;
        imageField.sprite = cardInfo.cardSprite;

        _cardInfo = cardInfo;
    }

    public void CheckForSelected()
    {
        if (IsSelected) ToggleOff();
    }

    public override void ExecuteOnDeselection()
    {
        IsSelected = false;
        BonusEffect();
    }

    public override void ExecuteOnSelection()
    {
        FindObjectOfType<UISelection>().OnBonusCardSelection();
        IsSelected = true;
        BonusEffect();
    }

    void BonusEffect()
    {
        Debug.Log("Is called with : " + IsSelected);
        effectManager.SelectEffect(_cardInfo, bonusEffect, IsSelected);
    }
}
