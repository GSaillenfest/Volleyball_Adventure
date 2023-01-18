using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIActionDisplay : MonoBehaviour
{
    [SerializeField]
    ActionAnimator actionAnimator;

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
        //button.GetComponent<ActionOnSelectionAnimation>().AnimateOnSelection(isSelected);
    }

    public void AnimateOnUnselectable(ActionRPA button, bool isSelectable)
    {

        if (!isSelectable)
        {
            button.GetComponentInChildren<TMP_Text>().color = new Color32(0, 0, 0, 100);
            Debug.Log(isSelectable);
        }
    }
}
