using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelection : MonoBehaviour
{

    public event Action<Effect> A_OnActionSelection;
    public event Action A_OnValidation;
    public event Action A_OnBonusCardSelection;

    List<ActionRPA> actionButtons = new();

    private void OnEnable()
    {
        actionButtons.AddRange(FindObjectsOfType<ActionRPA>());
    }

    public void OnActionSelection(Effect selectedEffectType)
    {
        A_OnActionSelection?.Invoke(selectedEffectType);
    }

    public void OnValidation()
    {
        int selected = 0;
        foreach (ActionRPA action in actionButtons)
        {
            if (action.IsSelected) selected++;
        }
        if (selected == 3)
            A_OnValidation?.Invoke();
    }

    public void OnBonusCardSelection()
    { 
        A_OnBonusCardSelection?.Invoke();
    }

    
}
