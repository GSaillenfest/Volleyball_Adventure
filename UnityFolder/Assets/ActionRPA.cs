using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ActionRPA : Effect, IPlayableEffect
{

    [SerializeField]
    public int powerValue;

    public override void OnEnable()
    {
        GameObject.Find("UI").GetComponent<UISelection>().AOnActionSelection += CheckForForbiddenSelection;
        base.OnEnable();
    }

    public override void ExecuteOnDeselection()
    {
        Calculation(0);
    }

    public override void ExecuteOnSelection()
    {
        GameObject.Find("UI").GetComponent<UISelection>().OnActionSelection(this);
        Calculation(powerValue);
        IsSelected = true;
    }

    void CheckForForbiddenSelection(Effect selectedEffectType)
    {
        if (selectedEffectType.transform.parent.Equals(transform.parent) && IsSelected)
        {
            if (selectedEffectType._actionType == _actionType + 1 || selectedEffectType._actionType == _actionType - 1)
            {
                Debug.Log("Uncheck : Same Player can't play twice in a row");
                ToggleOff();
                IsSelected = false;
            }
        }
        else if (isSelected && _actionType == selectedEffectType._actionType)
        {
            Debug.Log("Uncheck : Same Action Type");
            IsSelected = false;
            ToggleOff();
        }
    }

    void Calculation(int value)
    {
        switch (_actionType)
        {
            case ActionType.Reception:
                calculator.SetReceptionValue(value);
                break;
            case ActionType.Pass:
                calculator.SetPassValue(value);
                break;
            case ActionType.Attack:
                calculator.SetAttackValue(value);
                break;
            case ActionType.Other:
                break;
            default:
                break;
        }
    }

}
