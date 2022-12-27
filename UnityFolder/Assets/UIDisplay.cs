using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    TMP_Text scoreDisplay;
    int scoreValue = 0;

    public int ScoreValue 
    { 
        get {return scoreValue;} 
        set 
        {
            scoreValue = value;
            UpdateScore(scoreValue);
        } 
    }

    private void Awake()
    {
        scoreDisplay = GameObject.Find("Score").GetComponent<TMP_Text>();
        Debug.Assert(scoreDisplay != null);
    }

    private void Start()
    {
        GetComponent<UISelection>().AOnValidation += OnValidation;
        //GetComponent<UISelection>().AOnCardSelection += OnCardSelection;
        GetComponent<UISelection>().AOnBonusCardSelection += OnActionSelection;
        UpdateScore(scoreValue);
        
    }

    private void OnCardSelection()
    {
        UpdateScore(scoreValue); 
    }

    void OnActionSelection()
    {
        UpdateScore(scoreValue);
    }

    void OnValidation()
    {
        UpdateScore(scoreValue);
    }

    void UpdateScore(int value)
    {
        scoreDisplay.SetText("Puissance : " + value.ToString());
    }

}
