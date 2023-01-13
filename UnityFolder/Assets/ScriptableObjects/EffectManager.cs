using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    [SerializeField]
    Calculator calculator;

    List<ActionRPA> actions = new();
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
        actions.AddRange(FindObjectOfType<UISelection>().actionButtons);
        if (isActionConstrained)
        {
            foreach (ActionRPA action in actions)
            {
                action.IsSelected = false;
                selectableStates.Add(action.IsSelectable);
            }
            calculator.ResetValues();
            
            foreach (ActionRPA action in actions)
            {
                FindObjectOfType<UISelection>().A_OnActionSelection -= action.CheckForForbiddenSelection;
                if (action._actionType == actionType)
                {
                    action.IsSelectable = !action.IsSelectable;
                    if (action.IsSelectable) numberCanBeRestored++;
                }
                else
                {
                    action.IsSelectable = false;
                }
            }
            if (numberToRestore > numberCanBeRestored)
            {
                Debug.Log(numberCanBeRestored + "Number can be restored");
                numberToRestore = numberCanBeRestored;
            }
        }
        FindObjectOfType<UISelection>().A_OnActionSelection += CountRestore;
    }

    private void CountRestore(ActionRPA obj)
    {
        numberToRestore--;
        Debug.Log("Nombre de restorations restant : " + numberToRestore + obj.name);
        if (numberToRestore == 0)
        {
            FindObjectOfType<UISelection>().A_OnActionSelection -= CountRestore;
            RestoreAndResetActions();
        }
    }

    private void RestoreAndResetActions()
    {
        Debug.Log("ici ?");
        for (int i = 0; i < actions.Count; i++)
        {
            Debug.Log("reverse");
            if (actions[i]._actionType == _actionType)
            {
                if (!actions[i].IsSelected)
                {
                    Debug.Log("XX4");
                    actions[i].IsSelectable = !actions[i].IsSelectable;
                }
                else Debug.Log("XX6");
            }
            else
            {
                    Debug.Log("XX5");
                actions[i].IsSelectable = selectableStates[i];
            }
            actions[i].IsSelected = false;
            Debug.Log(actions[i].IsSelected);
            FindObjectOfType<UISelection>().A_OnActionSelection += actions[i].CheckForForbiddenSelection;
            Debug.Log(actions[i].IsSelected);
        }
    }

    void ReverseRestoreAction()
    {
        Debug.Log("isCalledReverse");
        FindObjectOfType<UISelection>().A_OnActionSelection -= CountRestore;
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].IsSelected = false;
            actions[i].IsSelectable = selectableStates[i];
            FindObjectOfType<UISelection>().A_OnActionSelection += actions[i].CheckForForbiddenSelection;
        }
        actions.Clear();
        selectableStates.Clear();
        numberToRestore = 0;
        numberCanBeRestored = 0;
    }
}
