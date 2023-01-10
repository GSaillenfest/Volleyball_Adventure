using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calculator : MonoBehaviour
{
    public int receptionValue;
    public int passValue;
    public int attackValue;
    public int receptionModifiedValue;
    public int passModifiedValue;
    public int attackModifiedValue;
    int scoreValue;

    public event Action ACallModifier;

   public void Calculate()
    {
        attackModifiedValue = attackValue;
        passModifiedValue = passValue;
        receptionModifiedValue = receptionValue;
        CallModifiers();
        scoreValue = receptionModifiedValue + passModifiedValue + attackModifiedValue;
        GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue = scoreValue;
    }

    void CallModifiers()
    {
        //Debug.Log("CallModifiers");
        ACallModifier?.Invoke();
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


    public void ResetValues()
    {
        receptionValue = 0;
        passValue = 0;
        attackValue = 0;
        scoreValue = receptionValue + passValue + attackValue;
        GameObject.Find("TeamUI").GetComponent<UIDisplay>().PowerValue = scoreValue;
    }
}
