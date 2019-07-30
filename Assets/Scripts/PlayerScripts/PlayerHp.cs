using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour {

    public int originalHp = 100; //оригинальное неизменяемое здоровьего объекта

    public int currentHp;        //здоровье, которое будет изменяться

    [SerializeField]
    public int power;

    public Slider hpBar;
    public GameObject menuContr;
    public GameObject bot;
    public GameObject playerOne;
    public float positionBot;
    public float positionPlayer;
    public float position;

    public Vector3 direction;

    public void Start()
    {
        currentHp = originalHp; //присваивание значения оригинального здоровья к currentHp
    }

    public /*bool*/ void SetDamage(int damage) //переменная damage типа int, через которую будет задавться наносимый урон в виде числа
    {
        Push();
        if (playerOne.GetComponent<Character>().block == false)
        {
            
            currentHp -= damage;
            hpBar.value = currentHp;

            //if (currentHp > 0)
            //{
            //    return true;
            //}
            if (currentHp <= 0)
            {
                menuContr.GetComponent<MenyControl>().DieWindow();
                playerOne.SetActive(false);
                //Destroy(gameObject);
                //Application.LoadLevel("Menu");
                //return false;
            }
        }
    }

    

    public void Push()
    {
        if (playerOne.GetComponent<Character>().block == true)
        {
            power = 10;
        }
        else
        {
            power = 20;
        }
        playerOne.GetComponent<Rigidbody2D>().AddForce(transform.right * position * power, ForceMode2D.Impulse);
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, power * Time.deltaTime);
    }

    public void Update()
    {
        direction = transform.right * position;
        positionPlayer = playerOne.transform.position.x;
        if (bot != null ) positionBot = bot.transform.position.x;
        position = (positionBot < positionPlayer) ? 1 : -1;
    }
}
