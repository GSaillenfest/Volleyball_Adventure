using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelection : MonoBehaviour
{
    EffectManager effectManager;
    Calculator calculator;
    public event Action A_OnValidation;
    public event Action<Card> A_OnBonusCardSelection;

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
        //if (hasSmash) effectManager.RestoreAction(ActionType.Other, false, 1);
    }

    public void OnTurnEnd()
    {
        // because of SetActive false while multiplayer is not implemented
        CheckForReplacement();
        actionButtons.Clear();
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
            if (selectedActionButton.transform.parent.transform.parent.Equals(action.transform.parent.transform.parent) && action.IsSelected)
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
        A_OnBonusCardSelection?.Invoke(cardToActivate);
    }

    // Outdated : new rule implementation >>> Players are replaced only when their 3 actions are not available, then they are replaced by a new team member
    // >>> CheckForReplacement()
    public void ResetSelectionState()
    {
        Debug.Log("Resetting");
        foreach (ActionRPA action in actionButtons)
        {
            action.IsSelected = false;
            action.IsSelectable = true;
        }
    }

    public void CheckForReplacement()
    {
        int iLibero = 0;
        int iBack = 0;
        int iSetter = 0;
        int iHitter = 0;
        foreach (ActionRPA action in actionButtons)
        {
            if (!action.IsSelectable)
            {
                switch (action.playerRole)
                {
                    case PlayerRole.Libero:
                        iLibero++;
                        if (iLibero == 3) ReplacePlayer(PlayerRole.Libero);
                        break;
                    case PlayerRole.Hitter:
                        iHitter++;
                        if (iHitter == 3) ReplacePlayer(PlayerRole.Hitter);
                        break;
                    case PlayerRole.Back:
                        iBack++;
                        if (iBack == 3) ReplacePlayer(PlayerRole.Back);
                        break;
                    case PlayerRole.Setter:
                        iSetter++;
                        if (iSetter == 3) ReplacePlayer(PlayerRole.Setter);
                        break;
                    default:
                        break;
                }
            }
            action.IsSelected = false;
        }

    }

    private void ReplacePlayer(PlayerRole playerRole)
    {
        foreach (ActionRPA action in actionButtons)
        {
            if (action.playerRole == playerRole)
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
                playerTags.Add(action.transform.parent.transform.parent.transform.parent.tag);
                Debug.Log(action.transform.parent.transform.parent.transform.parent.tag);
            }
        }
        return playerTags;
    }

    public void UIToggleSelectable(bool isSelectable, Button button)
    {
        button.interactable = isSelectable;
    }

}
