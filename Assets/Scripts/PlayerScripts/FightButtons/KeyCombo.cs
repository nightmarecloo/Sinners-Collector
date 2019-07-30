using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCombo
{
    public string[] buttons;
    private int currentIndex = 0; //moves along the array as buttons are pressed

    public GameObject y;
    public GameObject x;
    public GameObject a;
    public GameObject b;

    public float allowedTimeBetweenButtons = 0.3f; //tweak as needed
    private float timeLastButtonPressed;

    public KeyCombo(string[] b)
    {
        buttons = b;
    }

    //usage: call this once a frame. when the combo has been completed, it will return true
    public bool Check()
    {
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
        {
            if (currentIndex < buttons.Length)
            {
                y = GameObject.Find("Y");
                x = GameObject.Find("X");
                a = GameObject.Find("A");
                b = GameObject.Find("B");


                if ((buttons[currentIndex] == "y" && y.gameObject.GetComponent<ButtonController>().punchTiming == true) ||
                (buttons[currentIndex] == "x" && x.gameObject.GetComponent<ButtonController>().punchTiming == true) ||
                (buttons[currentIndex] == "a" && a.gameObject.GetComponent<ButtonController>().punchTiming == true) ||
                (buttons[currentIndex] == "b" && b.gameObject.GetComponent<ButtonController>().punchTiming == true) ||
                (buttons[currentIndex] != "y" && buttons[currentIndex] != "x" && buttons[currentIndex] != "a" && buttons[currentIndex] != "b" && Input.GetButtonDown(buttons[currentIndex])))
                {
                    timeLastButtonPressed = Time.time;
                    currentIndex++;
                }

                if (currentIndex >= buttons.Length)
                {
                    currentIndex = 0;
                    return true;
                }
                else return false;
            }
        }

        return false;
    }
}
