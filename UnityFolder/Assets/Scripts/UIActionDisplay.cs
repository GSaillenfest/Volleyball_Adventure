using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIActionDisplay : MonoBehaviour
{
    [SerializeField]
    public UIAnimator actionAnimator;    
    [SerializeField]
    public UIAnimator cardAnimator;

    UISelection uiSelection;

    TMP_Text turnPowerDisplay;
    int powerValue = 0;

    public int PowerValue 
    { 
        get {return powerValue;} 
        set 
        {
            powerValue = value;
            UpdateScore(powerValue);
        } 
    }

    private void Awake()
    {
        uiSelection = FindObjectOfType<UISelection>();
        turnPowerDisplay = GameObject.Find("PowerValue").GetComponent<TMP_Text>();
        Debug.Assert(turnPowerDisplay != null);
    }

    private void Start()
    {
        uiSelection.A_OnValidation += OnValidation;
        uiSelection.A_OnBonusCardSelection += OnCardSelection;
        uiSelection.A_OnBonusCardSelection += UICardSelection;
        UpdateScore(PowerValue);
    }

    private void OnCardSelection(Card selectedCard)
    {
        UpdateScore(PowerValue); 
    }

    void OnActionSelection()
    {
        UpdateScore(PowerValue);
    }

    void OnValidation()
    {
        UpdateScore(0);
    }

    void UpdateScore(int value)
    {
        turnPowerDisplay.SetText(value.ToString());
    }

    public void UICardSelection(Card selectedCard)
    {
        selectedCard.GetComponent<Card_AnimationOnSelection>().AnimateOnSelection(selectedCard.IsSelected);
    }

    //obsolete
    public void UIToggleSelectable(Animator animator, bool isSelectable, Button button)
    {
        if (animator == null) Debug.Log("pas de bouton");
        animator.SetBool("IsSelectable", isSelectable);
        Debug.Assert(uiSelection != null);
        uiSelection.UIToggleSelectable(isSelectable, button);
    }

    public void AnimateOnSelection(ActionRPA button, bool isSelected)
    {
        button.GetComponentInChildren<TMP_Text>().color = isSelected ? Color.white : Color.black;
        button.GetComponent<Action_AnimationOnSelection>().AnimateOnSelection(isSelected);
    }

    public void AnimateOnUnselectable(ActionRPA button, bool isSelectable)
    {

        if (!isSelectable)
        {
            button.GetComponentInChildren<TMP_Text>().color = new Color32(0, 0, 0, 100);
        }
    }
}
