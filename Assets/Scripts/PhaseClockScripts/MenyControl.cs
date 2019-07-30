using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenyControl : MonoBehaviour
{
    public GameObject pauseM;
    public GameObject clockM;
    public GameObject inventory;
    public GameObject eventItem;
    public GameObject allButtons;
    public GameObject dieWindow;
    
    public bool pauseMenu;
    public bool clockMenu;
    public bool invMenu;

    public void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Application.targetFrameRate = 60;
    }

    public void Start()
    {
        Screen.autorotateToLandscapeLeft = true;

        inventory.SetActive(false);
        clockM.SetActive(false);
    }

    public void PauseActvie()
    {
        pauseMenu = pauseMenu ? false : true;

        if (pauseMenu == true) 
        {   
            pauseM.SetActive(true);
            allButtons.SetActive(false);
            Time.timeScale = 0;
        }
        else 
        {
            pauseM.SetActive(false);
            allButtons.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void PhaseClockActive()
    {
        clockMenu = clockMenu ? false : true;

        if (clockMenu == true)
        {
            clockM.SetActive(true);
            allButtons.SetActive(false);
            Time.timeScale = 0;
        }
        else 
        {
            clockM.SetActive(false);
            allButtons.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void InventoryActive()
    {
        invMenu = invMenu ? false : true;

        if (invMenu == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    public void EventItem()
    {
        eventItem.SetActive(true);
    }

    public void Menu()
    {
        Application.LoadLevel("Menu");
        Time.timeScale = 1f;
    }

    public void Switch()
    {
        Application.LoadLevel("SampleScene");

    }
    public void Exit()
    {
        Application.Quit();
    }

    public void DieWindow()
    {
        dieWindow.SetActive(true);
        allButtons.SetActive(false);
    }

    
}
