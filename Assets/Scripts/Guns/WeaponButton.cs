using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Image m_Image;
    public Sprite gun_6;
    public GameObject playerOne;
    public int tap;
    public bool doublClick;

    private const float CheckPeriod = 0.5f;
    private float m_LastCheck = CheckPeriod;

    public void DoubleClick()
    {
        tap++;
        
        
    }

    private void Update()
    {
        if (tap != 0) m_LastCheck -= Time.deltaTime;
        if (m_LastCheck < 0 && tap == 1)
        {
            playerOne.GetComponent<Character>().ButtonTakeEnter();
            tap = 0;
            doublClick = true;//тупо и неудобно
            m_LastCheck = CheckPeriod;
        }
        if (doublClick == true && m_LastCheck > 0 && tap > 1)
        {
            playerOne.GetComponent<Character>().GunReload();
            tap = 0;
            m_LastCheck = CheckPeriod;
            doublClick = false;
        }
    }

    void Start()
    {
        m_Image = GetComponent<Image>();
    }
    
    public void Gun_6()
    {
        m_Image.sprite = gun_6;
    }
}
