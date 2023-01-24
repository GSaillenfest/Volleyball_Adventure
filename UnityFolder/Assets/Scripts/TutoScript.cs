using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;
    int index = 0;
    public List<ActionRPA> actionButtons = new();

    private void OnEnable()
    {
        panels[index].SetActive(true);
        actionButtons.AddRange(FindObjectsOfType<ActionRPA>());
    }

    public void Next()
    {
        index++;
        if (index < panels.Length)
        {
            panels[index].SetActive(true);
            panels[index - 1].SetActive(false);
        }
        else SceneManager.LoadScene(0);
    }

    public void ValidateButton_PanelTwo()
    {
        int selected = 0;
        foreach (ActionRPA action in actionButtons)
        {
            if (action.IsSelected) selected++;
        }
        if (selected == 3)
        {
            Next();
        }
    }
}
