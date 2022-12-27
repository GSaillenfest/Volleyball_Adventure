using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionRPA : Effect, IPlayableEffect
{

    [SerializeField]
    public int powerValue;

    void OnEnable()
    {
        // GameObject.Find("UI").GetComponent<UISelection>().AOnActionSelection += DeSelect;
    }

    public override void ExecuteOnDeselection()
    {
        Calculation(0);
    }

    public override void ExecuteOnSelection()
    {
        Calculation(powerValue);
        GameObject.Find("UI").GetComponent<UISelection>().OnActionSelection(this);
    }

    void DeSelect(Effect selectedEffectType)
    {
        if (selectedEffectType.transform.parent.Equals(transform.parent) && IsSelected)
        {
            if (selectedEffectType._actionType == _actionType + 1 || selectedEffectType._actionType == _actionType - 1)
            {
                ToggleOff();
                IsSelected = false;
            }
        }
        else if (isSelected && _actionType == selectedEffectType._actionType)
        {
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
