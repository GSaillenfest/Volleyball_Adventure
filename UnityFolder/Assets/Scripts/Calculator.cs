using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    
    public int receptionValue;
    public int passValue;
    public int attackValue;
    public int receptionModifiedValue;
    public int passModifiedValue;
    public int attackModifiedValue;
    int powerValue;

    public event Action A_CallModifier;

   public void Calculate()
    {
        attackModifiedValue = attackValue;
        passModifiedValue = passValue;
        receptionModifiedValue = receptionValue;
        CallModifiers();
        powerValue = receptionModifiedValue + passModifiedValue + attackModifiedValue;
        GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue = powerValue;
    }

    void CallModifiers()
    {
        //Debug.Log("CallModifiers");
        A_CallModifier?.Invoke();
    }

    void UpdateScore()
    {

    }

    public void SetReceptionValue(int value)
    {
        receptionValue = value;
        Calculate();
    }   
    
    public void SetPassValue(int value)
    {
        passValue = value;
        Calculate();
    }    
    
    public void SetAttackValue(int value)
    {
        attackValue = value;
        Calculate();
    }

    public void SetReceptionMod(int value)
    {
        receptionModifiedValue = value;
        Calculate();
    }    
    
    public void SetPassMod(int value)
    {
        passModifiedValue = value;
        Calculate();
    }    
    
    public void SetAttackMod(int value)
    {
        attackModifiedValue = value;
        Calculate();
    }

    public void ValidateScore()
    {
        gameManager.ValidateBallPower(powerValue);
    }

    public void ResetValues()
    {
        receptionValue = 0;
        passValue = 0;
        attackValue = 0;
        powerValue = receptionValue + passValue + attackValue;
        GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue = powerValue;
        A_CallModifier = null;
        Calculate();
    }
}
