using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHP : MonoBehaviour
{

    public int originalHp = 100; //оригинальное неизменяемое здоровьего объекта

    public int currentHp;        //здоровье, которое будет изменяться


    public float power = 20f;

    public GameObject bot;
    public GameObject playerOne;
    public float positionBot;
    public float positionPlayer;
    public float position;

    public Vector3 direction;

    void Start()
    {
        playerOne = GameObject.Find("PlayerOne");
        currentHp = originalHp;
    }

    public /*bool*/ void SetDamage(int damage) //переменная damage типа int, через которую будет задавться наносимый урон в виде числа
    {
        Push();
        currentHp -= damage;
        

        //if (currentHp > 0)
        //{
        //    return true;
        //}
        if (currentHp <= 0)
        {

            Destroy(gameObject);
            
            //return false;
        }
    }
    
    public void Push()
    {
        bot.GetComponent<Rigidbody2D>().AddForce(transform.right * position * power, ForceMode2D.Impulse);
    }
    void Update()
    {
        direction = transform.right * position;
        if (playerOne != null) positionPlayer = playerOne.transform.position.x;
        positionBot = bot.transform.position.x;
        position = (positionBot < positionPlayer) ? -1 : 1;
    }
}
