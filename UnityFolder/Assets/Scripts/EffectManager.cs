using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    [SerializeField]
    Calculator calculator;

    List<ActionRPA> actionButtons = new();
    //List<ActionRPA> actionsToRestore = new();
    List<bool> selectableStates = new();
    int numberToRestore;
    int value;
    int numberCanBeRestored = 0;
    ActionType _actionType;
    CardBonusEffect _bonusEffect;

    public void SelectEffect(CardInfo cardInfo, CardBonusEffect bonusEffect)
    {
        UnapplyEffect(_bonusEffect);
        _bonusEffect = bonusEffect;
        ApplyEffect(cardInfo, _bonusEffect);
    }

    void UnapplyEffect(CardBonusEffect bonusEffect = CardBonusEffect.None)
    {
        switch (bonusEffect)
        {
            case CardBonusEffect.Add2PointsToAttackPlayer:
                calculator.A_CallModifier -= SuperAttack;
                calculator.Calculate();
                value = 0;
                break;
            case CardBonusEffect.Reset3Reception:
                ReverseRestoreAction();
                break;
            case CardBonusEffect.Reset2Passes:
                ReverseRestoreAction();
                break;
            case CardBonusEffect.Reset1Attack:
                ReverseRestoreAction();
                break;
            case CardBonusEffect.CloneNextEffect:
                break;
            case CardBonusEffect.DropBallPowerTo10:
                break;
            case CardBonusEffect.ConterAttackPlus3:
                break;
            case CardBonusEffect.InvalidateReceptionPlayer:
                break;
            case CardBonusEffect.DoubleReceptionValue:
                break;
            case CardBonusEffect.Discard1CardFromOponent:
                break;
            case CardBonusEffect.DoSmashIfPowerValueIsEqual:
                break;
            case CardBonusEffect.Add2PointsToSpecialities_RPA:
                break;
            case CardBonusEffect.OutIfOponnentBallIsWeak:
                break;
            case CardBonusEffect.DoublePassValueNextTurn:
                break;
            case CardBonusEffect.Ace:
                break;
            case CardBonusEffect.Discard2CardsAndPick2:
                break;
            default:
                break;
        }
    }

    private void ApplyEffect(CardInfo cardInfo, CardBonusEffect bonusEffect)
    {
        switch (bonusEffect)
        {
            case CardBonusEffect.Add2PointsToAttackPlayer:
                value = cardInfo.value;
                calculator.A_CallModifier += SuperAttack;
                calculator.Calculate();
                break;
            case CardBonusEffect.Reset3Reception:
                RestoreAction(ActionType.Reception, true, 3);
                break;
            case CardBonusEffect.Reset2Passes:
                RestoreAction(ActionType.Pass, true, 2);
                break;
            case CardBonusEffect.Reset1Attack:
                RestoreAction(ActionType.Attack, true, 1);
                break;
            case CardBonusEffect.CloneNextEffect:
                break;
            case CardBonusEffect.DropBallPowerTo10:
                break;
            case CardBonusEffect.ConterAttackPlus3:
                break;
            case CardBonusEffect.InvalidateReceptionPlayer:
                break;
            case CardBonusEffect.DoubleReceptionValue:
                break;
            case CardBonusEffect.Discard1CardFromOponent:
                break;
            case CardBonusEffect.DoSmashIfPowerValueIsEqual:
                break;
            case CardBonusEffect.Add2PointsToSpecialities_RPA:
                break;
            case CardBonusEffect.OutIfOponnentBallIsWeak:
                break;
            case CardBonusEffect.DoublePassValueNextTurn:
                break;
            case CardBonusEffect.Ace:
                break;
            case CardBonusEffect.Discard2CardsAndPick2:
                break;
            default:
                break;
        }
    }

    void SuperAttack()
    {
        Debug.Log("isActiveAndEnabled called");
        if (FindObjectOfType<UISelection>().ReturnPlayersSelected().Contains("AttackPlayer"))
        {
            int newValue = calculator.attackValue + value;
            calculator.attackModifiedValue = newValue;
        }
        else Debug.Log("not attack player");
    }

    void RestoreAction(ActionType actionType, bool isActionConstrained, int number)
    {
        Debug.Log("isCalled");
        numberToRestore = number;
        numberCanBeRestored = 0;
        _actionType = actionType;
        actionButtons.AddRange(FindObjectOfType<UISelection>().actionButtons);
        if (isActionConstrained)
        {
            // Deselect every buttons and memories their state
            foreach (ActionRPA actionButton in actionButtons)
            {
                actionButton.IsSelected = false;
                selectableStates.Add(actionButton.IsSelectable);
            }
            calculator.ResetValues();

            // Set Selectable only action buttons matching ActionType condition
            foreach (ActionRPA actionButton in actionButtons)
            {
                //FindObjectOfType<UISelection>().A_OnActionSelection -= actionButton.CheckForForbiddenSelection;
                if (actionButton._actionType == actionType)
                {
                    actionButton.IsSelectable = !actionButton.IsSelectable;
                    if (actionButton.IsSelectable) numberCanBeRestored++;
                }
                else
                {
                    actionButton.IsSelectable = false;
                }
                // Set buttons to a RestoreState (no calculation when clicked and no check for forbidden selection)
                actionButton.SetToRestoreState();
            }
            // Limit number of restorable buttons 
            if (numberToRestore > numberCanBeRestored)
            {
                Debug.Log(numberCanBeRestored + "buttons can be restored");
                numberToRestore = numberCanBeRestored;
            }
        }
    }

    public void CountRestore(ActionRPA actionToRestore)
    {
        numberToRestore--;
        Debug.Log("Nombre de restorations restant : " + numberToRestore + actionToRestore.name);
        if (numberToRestore == 0)
        {
            RestoreAndResetActions();
        }
    }

    private void RestoreAndResetActions()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            // Reset back IsSelectable to other buttons, or set it true if selected to be restored
            if (actionButtons[i]._actionType == _actionType)
            {
                if (actionButtons[i].IsSelected)
                {
                    selectableStates[i] = !selectableStates[i];
                }
            }
            // Deselected every button
            actionButtons[i].IsSelected = false;
            actionButtons[i].IsSelectable = selectableStates[i];
            // Set them back to normal state
            actionButtons[i].SetToNormalState();
        }
    }

    void ReverseRestoreAction()
    {
        Debug.Log("isCalledReverse");
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].SetToNormalState();
            actionButtons[i].IsSelected = false;
            actionButtons[i].IsSelectable = selectableStates[i];
        }
        actionButtons.Clear();
        selectableStates.Clear();
        numberToRestore = 0;
        numberCanBeRestored = 0;
    }
}
