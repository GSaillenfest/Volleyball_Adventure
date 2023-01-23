using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IPlayableEffect
{
    void ExecuteOnSelection();
    void ExecuteOnDeselection();
}

public enum PlayerRole
{
    Unassigned = 0,
    Libero,
    Hitter,
    Back,
    Setter
}

public class ActionRPA : MonoBehaviour, IPlayableEffect
{
    [SerializeField]
    public int powerValue;

    [SerializeField]
    public ActionType _actionType;

    [SerializeField]
    public OperatorType _operatorType;

    [SerializeField]
    protected Calculator calculator;

    public PlayerRole playerRole;

    protected Animator animator;
    
    UIActionDisplay uiDisplay;

    protected bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            uiDisplay.AnimateOnSelection(this, isSelected);
            //uiDisplay.UIToggleSelection(this.GetComponent<Animator>(), isSelected);
        }
    }

    protected bool isSelectable = true;
    public bool IsSelectable
    {
        get { return isSelectable; }
        set
        {
            isSelectable = value;
            uiDisplay.AnimateOnUnselectable(this, isSelectable);
            GetComponent<Button>().interactable = isSelectable;
            //uiDisplay.UIToggleSelectable(GetComponent<Animator>(), isSelectable, GetComponent<Button>());
        }
    }

    #region Methods

    private void Awake()
    {
        SetToNormalState();
        animator = GetComponent<Animator>();
        uiDisplay = FindObjectOfType<UIActionDisplay>();
        Debug.Assert(uiDisplay != null);
        switch (transform.tag)
        {
            case "LiberoPlayer":
                playerRole = PlayerRole.Libero;
                break;
            case "ReceptionPlayer":
                playerRole = PlayerRole.Back;
                break;
            case "PassPlayer":
                playerRole = PlayerRole.Setter;
                break;
            case "AttackPlayer":
                playerRole = PlayerRole.Hitter;
                break;
            default:
                break;
        }
    }

    public void SetToRestoreState()
    {
        GetComponent<Button>().onClick.RemoveListener(CheckForSelectionOnClick);
        GetComponent<Button>().onClick.AddListener(RestoreAction);
    }

    public void SetToNormalState()
    {
        GetComponent<Button>().onClick.RemoveListener(RestoreAction);
        GetComponent<Button>().onClick.AddListener(CheckForSelectionOnClick);
    }

    private void RestoreAction()
    {
        IsSelected = true;
        FindObjectOfType<UISelection>().RestoreAction(this);
    }

    private void CheckForSelectionOnClick()
    {
        if (IsSelected)
        {
            Toggle_Off();
        }
        else Toggle_On();
    }


    public void OnEnable()
    {
        GameObject.Find("TeamUI").GetComponent<UISelection>().A_OnValidation += CheckIfSelectedOnValidation;
        IsSelectable = isSelectable;
    }

    private void CheckIfSelectedOnValidation()
    {
        if (IsSelected)
        {
            IsSelectable = false;
            //IsSelected = false;
        }
    }

    public void ExecuteOnDeselection()
    {
        IsSelected = false;
        Calculation(0);
    }

    public void ExecuteOnSelection()
    {
        FindObjectOfType<UISelection>().OnActionSelection(this);
        IsSelected = true;
        Calculation(powerValue);
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

    //Relevant only if local multiplayer mode
    public void OnDisable()
    {
        //GetComponent<Button>().onClick.RemoveListener(CheckForSelectedOnClick);
    }


    public void Toggle_Off()
    {
        ExecuteOnDeselection();
    }

    public void Toggle_On()
    {
        ExecuteOnSelection();
    }

    // Start is called before the first frame update
    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
        uiDisplay = FindObjectOfType<UIActionDisplay>();
        animator = GetComponent<Animator>();
    }
    #endregion
}
