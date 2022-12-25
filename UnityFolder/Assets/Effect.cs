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
    [SerializeField]
    public ActionType _actionType;

    [SerializeField]
    public OperatorType _operatorType;

    protected bool isSelected;
    protected bool IsSelected { get { return isSelected; } set { isSelected = value; GetComponent<Button>().interactable = !isSelected; } }

    public abstract void ExecuteOnDeselection();

    public abstract void ExecuteOnSelection();
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
