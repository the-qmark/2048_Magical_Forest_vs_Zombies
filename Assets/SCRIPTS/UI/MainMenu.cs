using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;


    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
