using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject scoreBoard;

    TMP_Text scoreDisplay;
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
        scoreDisplay = GameObject.Find("PowerValue").GetComponent<TMP_Text>();
        Debug.Assert(scoreDisplay != null);
    }

    private void Start()
    {
        GetComponent<UISelection>().A_OnValidation += OnValidation;
        //GetComponent<UISelection>().AOnCardSelection += OnCardSelection;
        GetComponent<UISelection>().A_OnBonusCardSelection += OnActionSelection;
        UpdateScore(powerValue);
        
    }

    private void OnCardSelection()
    {
        UpdateScore(powerValue); 
    }

    void OnActionSelection()
    {
        UpdateScore(powerValue);
    }

    void OnValidation()
    {
        UpdateScore(powerValue);
    }

    void UpdateScore(int value)
    {
        scoreDisplay.SetText(value.ToString());
    }

    public void UIToggleSelection(Animator animator, bool isSelected)
    {
        animator.SetBool("IsSelected", isSelected);
    }

    public void UIToggleSelectable(Animator animator, bool isSelectable)
    {
        animator.SetBool("IsSelectable", isSelectable);
    }

    void UIChangeScoreBoard()
    {
        //scoreBoard.GetComponent<Image>().color = ;
    }

}
