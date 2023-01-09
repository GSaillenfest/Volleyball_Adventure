using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCard_SuperAttack : Effect
{
    int value = 2;

    public override void OnEnable()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnBonusCardSelection += CheckForSelected;
        base.OnEnable();
    }
    
    public void CheckForSelected()
    {
        if (IsSelected) ToggleOff();
    }

    public override void ExecuteOnDeselection()
    {
        IsSelected = false;
        calculator.ACallModifier -= BonusEffect;
        calculator.Calculate();
    }

    public override void ExecuteOnSelection()
    {
        FindObjectOfType<UISelection>().OnBonusCardSelection();
        isSelected = true;
        calculator.ACallModifier += BonusEffect;
        calculator.Calculate();
    }

    void BonusEffect()
    {
        Debug.Log("EffectApplied");
        int newValue = calculator.attackValue + value;
        calculator.attackModifiedValue = newValue;
    }


}
