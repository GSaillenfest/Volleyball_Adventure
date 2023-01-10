using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelection : MonoBehaviour
{

    public event Action<CardsAndActions> A_OnActionSelection;
    public event Action A_OnValidation;
    public event Action A_OnBonusCardSelection;

    List<ActionRPA> actionButtons = new();

    private void OnEnable()
    {
        actionButtons.AddRange(FindObjectsOfType<ActionRPA>());
    }

    public void OnTurnEnd()
    {
        actionButtons.Clear();
        A_OnActionSelection = null;
        A_OnValidation = null;
        A_OnBonusCardSelection = null;
    }

    public void OnActionSelection(CardsAndActions selectedEffectType)
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

    public void ResetSelectionState()
    {
        Debug.Log("Resetting");
        foreach (ActionRPA action in actionButtons)
        {
            action.IsSelected = false;
            action.IsSelectable = true;
        }
    }

    public List<string> ReturnPlayersSelected()
    {
        Debug.Log("returning tags");
        List<string> playerTags = new();
        foreach (ActionRPA action in actionButtons)
        {
            if (action.IsSelected)
                playerTags.Add(action.transform.parent.tag);
        }
        return playerTags;
    }
}