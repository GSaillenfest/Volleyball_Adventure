using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{

    public event Action<ActionRPA> A_OnActionSelection;
    public event Action A_OnValidation;
    public event Action A_OnBonusCardSelection;

    public List<ActionRPA> actionButtons = new();

    public bool _isActionConstrained;
    public ActionType _actionType;

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

    public void OnActionSelection(ActionRPA selectedEffectType)
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
        Debug.Log(selected);
        if (selected == 3)
            A_OnValidation?.Invoke();
    }

    public void OnBonusCardSelection(Card cardToActivate)
    { 
        A_OnBonusCardSelection?.Invoke();
        cardToActivate.IsSelected = true;
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

    // Return tags of selected players
    public List<string> ReturnPlayersSelected()
    {
        List<string> playerTags = new();
        foreach (ActionRPA action in actionButtons)
        {
            if (action.IsSelected)
            {
                playerTags.Add(action.transform.parent.tag);
                Debug.Log(action.transform.parent.tag);
            }
        }
        return playerTags;
    }

    public void UIToggleSelectable(bool isSelectable, Button button)
    {
        button.interactable = isSelectable;
    }

    //public void ReverseToggleSelectableAll(ActionType actionType, bool isActionConstrained = false, int number = 1)
    //{
    //    maxNumberOfActionRestored = number;
    //    _isActionConstrained = isActionConstrained;
    //    _actionType = actionType;
    //    foreach (ActionRPA actionButton in actionButtons)
    //    {
    //        if (isActionConstrained)
    //        {
    //            if (actionButton._actionType == actionType)
    //                actionButton.IsSelectable = !actionButton.IsSelectable;
    //        }
    //        else
    //            actionButton.IsSelectable = !actionButton.IsSelectable;
    //    }
    //    A_OnActionSelection += ActionToResetList;
    //}

    //private void ActionToResetList(ActionRPA selectedEffectType)
    //{
    //    actionToRestore.Add(selectedEffectType);
    //    maxNumberOfActionRestored--;
    //    if (maxNumberOfActionRestored == 0)
    //    {
    //        A_OnActionSelection -= ActionToResetList;
    //        foreach (ActionRPA actionButton in actionButtons)
    //        {
    //            if (_isActionConstrained)
    //            {
    //                if (actionButton._actionType == _actionType)
    //                    actionButton.IsSelectable = !actionButton.IsSelectable;
    //            }
    //            else
    //                actionButton.IsSelectable = !actionButton.IsSelectable;
    //        }
    //    }
    //}
}
