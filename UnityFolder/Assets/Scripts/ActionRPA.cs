using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRPA : Effect, IPlayableEffect
{

    [SerializeField]
    public int powerValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnable()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnActionSelection += CheckForForbiddenSelection;
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnValidation += CheckForSelected;
        base.OnEnable();
    }

    private void CheckForSelected()
    {
        Debug.Log("Checking");
        if (IsSelected)
            IsSelectable = false;
    }

    public override void ExecuteOnDeselection()
    {
        Calculation(0);
        IsSelected = false;
    }

    public override void ExecuteOnSelection()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().OnActionSelection(this);
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
