using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRPA : ActionBehaviour, IPlayableEffect
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
        if (IsSelected)
            IsSelectable = false;
    }

    public override void ExecuteOnDeselection()
    {
        IsSelected = false;
        Calculation(0);
    }

    public override void ExecuteOnSelection()
    {
        IsSelected = true;
        FindObjectOfType<UISelection>().OnActionSelection(this);
        Debug.Log(this.transform.name + this.transform.parent.transform.parent.name);
        Calculation(powerValue);
    }

    public void CheckForForbiddenSelection(ActionBehaviour selectedEffectType)
    {
        if (selectedEffectType.transform.parent.Equals(transform.parent) && IsSelected)
        {
            if (selectedEffectType._actionType == _actionType + 1 || selectedEffectType._actionType == _actionType - 1)
            {
                //Debug.Log("Uncheck : Same Player can't play twice in a row");
                ToggleOff();
            }
        }
        else if (isSelected && _actionType == selectedEffectType._actionType)
        {
            //Debug.Log("Uncheck : Same Action Type");
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
