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
        GameObject.Find("UI").GetComponent<UISelection>().AOnCardSelection += DeSelect;
        GameObject.Find("UI").GetComponent<UISelection>().AOnCardSelection += SetUnselectable;
    }

    public override void ExecuteOnDeselection()
    {
        IsSelected = false;
        FindObjectOfType<Calculator>().CalculateOnClick(_actionType, _operatorType +1, powerValue);
    }

    public override void ExecuteOnSelection()
    {
        if (isSelected) return;
        GameObject.Find("UI").GetComponent<UISelection>().OnCardSelection(this);
        GetComponent<Button>().Select();
        IsSelected = true;
        FindObjectOfType<Calculator>().CalculateOnClick(_actionType, _operatorType, powerValue);
    }

    void DeSelect(Effect selectedEffectType)
    {
        if (isSelected && _actionType == selectedEffectType._actionType)
        {
            ExecuteOnDeselection();
            IsSelected = false;
        }
    }

    void SetUnselectable(Effect selectedEffectType)
    {
        if (selectedEffectType.transform.parent.Equals(transform.parent))
            {
            if (selectedEffectType._actionType == _actionType + 1 || selectedEffectType._actionType == _actionType - 1)
            {
                if (isSelected)
                ExecuteOnDeselection();
                IsSelected = false;
            }
        }
    }
}
