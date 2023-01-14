using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    EffectManager effectManager;
    Calculator calculator;
    public event Action<ActionRPA> A_OnActionSelection;
    public event Action A_OnValidation;
    public event Action A_OnBonusCardSelection;

    public List<ActionRPA> actionButtons = new();

    public bool _isActionConstrained;
    public ActionType _actionType;

    private void Awake()
    {
        calculator = FindObjectOfType<Calculator>();
        effectManager = FindObjectOfType<EffectManager>();
    }

    private void OnEnable()
    {
        actionButtons.AddRange(FindObjectsOfType<ActionRPA>());
    }

    public void OnTurnEnd()
    {
        // because of SetActive false while multiplayer is not implemented
        actionButtons.Clear();
        A_OnActionSelection = null;
        A_OnValidation = null;
        A_OnBonusCardSelection = null;
    }

    public void RestoreAction(ActionRPA actionToRestore)
    {
        effectManager.CountRestore(actionToRestore);
    }

    public void OnActionSelection(ActionRPA selectedActionButton)
    {
        DeselectedForbiddenActions(selectedActionButton);
    }

    public void DeselectedForbiddenActions(ActionRPA selectedActionButton)
    {
        foreach (ActionRPA action in actionButtons)
        {
            if (selectedActionButton.transform.parent.Equals(action.transform.parent) && action.IsSelected)
            {
                if (selectedActionButton._actionType == action._actionType + 1 || selectedActionButton._actionType == action._actionType - 1)
                {
                    //Debug.Log("Uncheck : Same Player can't play twice in a row");
                    action.Toggle_Off();
                }
            }
            else if (action.IsSelected && action._actionType == selectedActionButton._actionType)
            {
                //Debug.Log("Uncheck : Same Action Type");
                action.Toggle_Off();
            }
        }
    }

    public void OnValidation()
    {
        int selected = 0;
        foreach (ActionRPA action in actionButtons)
        {
            if (action.IsSelected) selected++;
        }
        if (selected == 3)
        {
            A_OnValidation?.Invoke();
            calculator.ValidateScore();
        }
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
