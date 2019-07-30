using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    //этот скрипт вообще висит на пустом (невидимом) объекте, который находится на сцене неважно в каком месте

    public GameObject playerOne;  //в эти переменные в инспекторе данного объекта нужно перенести нужные объекты (персонажи) из проекта, в unity выбрав этот пустой объект на сцене
    public GameObject bot;
    public GameObject gun;

	void Start () {
        //Instantiate(playerOne, new Vector3(-3, 0, 0), Quaternion.identity);      //спавн одной строкой
        //Instantiate(gun, new Vector3(0, -2, 0), Quaternion.identity);
        //Instantiate(bot, new Vector3(5, 0, 0), Quaternion.identity);
    }
}
