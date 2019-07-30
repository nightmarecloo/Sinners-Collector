using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTouch : MonoBehaviour
{

    public void Shoot() //стрельба при косании панели
    {
        if (GameObject.Find("PlayerOne").GetComponent<Character>().give == true)
        {
            GameObject.Find("PlayerOne").GetComponent<Character>().subject.GetComponent<Weapon2D>().shoot = GameObject.Find("PlayerOne").GetComponent<Character>().subject.GetComponent<Weapon2D>().shoot ? false : true;
        }
    }
}
