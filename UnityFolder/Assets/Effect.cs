using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IPlayableEffect
{
    void ExecuteOnSelection();
    void ExecuteOnDeselection();
}


public abstract class Effect : MonoBehaviour, IPlayableEffect
{
    public virtual void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ToggleOn);
    }

    [SerializeField]
    public ActionType _actionType;

    [SerializeField]
    public OperatorType _operatorType;

    [SerializeField]
    protected Calculator calculator;

    protected bool isSelected;
    protected bool IsSelected { get { return isSelected; } set { isSelected = value; } }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ExecuteOnSelection();

    public abstract void ExecuteOnDeselection();
}
