using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Card class is a component of the Card Prefab
public class Card : MonoBehaviour//, IPlayableEffect
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

    CardBonusEffect bonusEffect;

    [SerializeField]
    Animator animator;

    [SerializeField]
    EffectManager effectManager;

    CardInfo _cardInfo;

    UISelection uiSelection;
    UIActionDisplay uiDisplay;

    bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            uiDisplay.UIToggleSelection(animator, IsSelected);
        }
    }

    #endregion

    private void Awake()
    {
        uiSelection = FindObjectOfType<UISelection>();
        uiDisplay = FindObjectOfType<UIActionDisplay>();
        effectManager = FindObjectOfType<EffectManager>();
        GetComponent<Button>().onClick.AddListener(CheckForSelectedOnCLick);
    }

    private void OnEnable()
    {
        uiSelection.A_OnBonusCardSelection += Toggle_Off;
        uiSelection.A_OnValidation += Discard;
    }

    private void Discard()
    {
        Debug.Log("must destroy");
        if (IsSelected) transform.parent.gameObject.SetActive(false);
    }

    public void Setup(CardInfo cardInfo)
    {
        _cardInfo = cardInfo;
        cardNameField.SetText(_cardInfo.cardName);
        subtitleField.SetText(_cardInfo.subtitle);
        descriptionField.SetText(_cardInfo.description);
        bonusEffect = _cardInfo.bonusEffect;
        imageField.sprite = _cardInfo.cardSprite;
    }

    public void CheckForSelectedOnCLick()
    {
        if (IsSelected)
        {
            Toggle_Off();
            BonusEffect(CardBonusEffect.None);
        }
        else Toggle_On();
    }

    private void Toggle_On()
    {
        FindObjectOfType<UISelection>().OnBonusCardSelection(this);
        BonusEffect(bonusEffect);
    }

    private void Toggle_Off()
    {
            Debug.Log("Off"  + _cardInfo.cardName);
            IsSelected = false;
    }


    void BonusEffect(CardBonusEffect bonusEffect)
    {
        Debug.Log("Is called with : " + bonusEffect);
        effectManager.SelectEffect(_cardInfo, bonusEffect);
    }

    //CardSelection
    //    ButtonOnClick
    //        CheckForSelected
    //            ifSelected
    //                ToggleOffThisCard
    //                    PlayAnimation
    //                    Deselect
    //                    ---U-n-a-p-p-l-y-E-f-f-e-c-t--- 
    //            Else
    //                ToggleOnThisCardAsParameter
    //                    PlayAnimation
    //                    IsSelectedTrue
    //                    ApplyEffect //risk of crossed commands (WaitForEndOfFrame ?) > UnApplyEffect(previousEffect) then ApplyEffect(effect)
    //                        UpdateCalculation?
    //                InvokeOnCardSelection
    //                Receive OnCardSelection

}
