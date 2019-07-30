using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public const float CheckPeriod = 0.2f;      //период между ударами
    public float m_LastCheck = CheckPeriod;     // переменные для задержки действия
    public GameObject player;

    public bool punchButton;
    public bool punchTiming;

    public void ButtonClickY()
    {
        player.GetComponent<Character>().punch = true;
        punchTiming = true;
        player.GetComponent<Character>().p2 = true;
        m_LastCheck = CheckPeriod;
    }

    public void ButtonClickB()
    {
        player.GetComponent<Character>().punch = true;
        punchTiming = true;
        player.GetComponent<Character>().p3 = true;
        m_LastCheck = CheckPeriod;
    }

    public void ButtonClickX()
    {
        player.GetComponent<Character>().punch = true;
        punchTiming = true;
        player.GetComponent<Character>().p1 = true;
        m_LastCheck = CheckPeriod;
    }



    public void ButtonClickBlock()
    {
        player.GetComponent<Character>().block = true;
        player.GetComponent<Character>().punch = false;
    }

    public void ButtonExit()
    {
        punchTiming = false;
        player.GetComponent<Character>().block = false;
    }











    public void Update()
    {
        if (punchTiming == true)
        {
            m_LastCheck -= Time.deltaTime;
            if (m_LastCheck > 0)
            {
                punchTiming = false;
            }
        }
    }
}
