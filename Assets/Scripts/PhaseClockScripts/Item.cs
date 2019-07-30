using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public SpriteRenderer sprite;
    public bool itemBool;

    public Collider2D loot;
    public GameObject player;
    public bool activeItem;

    public void Start()
    {
        itemBool = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ItemActive()
    {
        activeItem = activeItem ? false : true;
        if (activeItem == true)
        {
            loot.gameObject.SetActive(true);
        }
        else
        {
            loot.gameObject.SetActive(false);
        }
    }
}
