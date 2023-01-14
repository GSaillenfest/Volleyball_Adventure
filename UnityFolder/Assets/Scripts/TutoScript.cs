using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;
    int index = 0;

    private void OnEnable()
    {
        panels[index].SetActive(true);
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
}
