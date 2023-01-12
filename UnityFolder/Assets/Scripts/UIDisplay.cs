using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
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
        uiSelection = GetComponent<UISelection>();
        turnPowerDisplay = GameObject.Find("PowerValue").GetComponent<TMP_Text>();
        Debug.Assert(turnPowerDisplay != null);
    }

    private void Start()
    {
        uiSelection.A_OnValidation += OnValidation;
        uiSelection.A_OnBonusCardSelection += OnActionSelection;
        UpdateScore(PowerValue);
    }

    private void OnCardSelection()
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

    public void UIToggleSelection(Animator animator, bool isSelected)
    {
        animator.SetBool("IsSelected", isSelected);
    }

    public void UIToggleSelectable(Animator animator, bool isSelectable, Button button)
    {
        animator.SetBool("IsSelectable", isSelectable);
        uiSelection.UIToggleSelectable(isSelectable, button);
    }
}
