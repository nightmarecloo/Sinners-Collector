using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour {

    public Collider2D subject;
    public Collider2D touchSubject; //предмет, который игрок может взять
    public Transform newParent;
    
    public bool take;
    public bool hasLoot;
    
    public void Start()
    {
        take = false;   //есть ли предмет в руках
        hasLoot = true; //могу ли подобрать предмет
    }

    public void TakeSubject()
    {
        if (subject != null && take == false && subject.gameObject.tag == "Loot")
        {
            subject.transform.SetParent(newParent);
            subject.GetComponent<Weapon2D>().flipParent = newParent;
            subject.GetComponent<Gun>().PhysicsKinematic();
            subject.gameObject.tag = "MyLoot";      //чтобы нельзя было взять предмет из рук противника
            subject.gameObject.layer = 13;
            hasLoot = false;
            subject.GetComponent<Loot>().loot.SetActive(false); //выключает подобранный объект
        }

        if (take == true)
        {
            UnTake();
        }
    }

    public void UnTake()
    {
        subject.GetComponent<Weapon2D>().flipParent = null;
        subject.transform.SetParent(null);
        subject.GetComponent<Gun>().PhysicsDynamic();
        subject.gameObject.tag = "Loot";
        subject.gameObject.layer = 10;
        hasLoot = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (touchSubject == null && collision.gameObject.tag == "Loot")
        {
            touchSubject = collision;
        }
        if (subject == null && hasLoot == true && collision.gameObject.tag == "Loot") subject = collision;
    }



    public void OnTriggerExit2D(Collider2D collision)
    {
        if (hasLoot == true) subject = null;
        touchSubject = null;
    }
}
