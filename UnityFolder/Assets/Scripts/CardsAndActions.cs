using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IPlayableEffect
{
    void ExecuteOnSelection();
    void ExecuteOnDeselection();
}


public abstract class CardsAndActions : MonoBehaviour, IPlayableEffect
{

    [SerializeField]
    public ActionType _actionType;

    [SerializeField]
    public OperatorType _operatorType;

    [SerializeField]
    protected Calculator calculator;

    protected bool isSelected;
    public bool IsSelected 
    { 
        get { return isSelected; } 
        set 
        { 
            isSelected = value;
            uiDisplay.UIToggleSelection(this.GetComponent<Animator>(), isSelected);
        }
    }
    
    protected bool isSelectable;
    public bool IsSelectable 
    { 
        get { return isSelectable; } 
        set 
        { 
            isSelectable = value;
            uiDisplay.UIToggleSelectable(this.GetComponent<Animator>(), isSelectable, GetComponent<Button>());
        }
    }

    protected Animator animator;
    UIDisplay uiDisplay;

    public virtual void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ToggleOn);
    }

    //Relevant only if local multiplayer mode
    public virtual void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ToggleOn);
    }

    public virtual void ToggleOff()
    {
        ExecuteOnDeselection();
        GetComponent<Button>().onClick.RemoveListener(ToggleOff);
        GetComponent<Button>().onClick.AddListener(ToggleOn);
    }

    public virtual void ToggleOn()
    {
        ExecuteOnSelection();
        GetComponent<Button>().onClick.RemoveListener(ToggleOn);
        GetComponent<Button>().onClick.AddListener(ToggleOff);
        uiDisplay.UIToggleSelection(this.GetComponent<Animator>(), isSelected);
    }

    // Start is called before the first frame update
    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
        uiDisplay = FindObjectOfType<UIDisplay>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ExecuteOnSelection();

    public abstract void ExecuteOnDeselection();
}
