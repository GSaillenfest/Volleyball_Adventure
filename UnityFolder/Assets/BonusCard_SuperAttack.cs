using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCard_SuperAttack : Effect
{
    int value = 2;


    public override void ExecuteOnDeselection()
    {
        calculator.ACallModifier -= BonusEffect;
        calculator.Calculate();
    }

    public override void ExecuteOnSelection()
    {
        calculator.ACallModifier += BonusEffect;
        FindObjectOfType<UISelection>().OnBonusCardSelection();
    }

    void BonusEffect()
    {
        Debug.Log("EffectApplied");
        int newValue = calculator.attackValue + 2;
        calculator.attackModifiedValue = newValue;
    }

    
}
