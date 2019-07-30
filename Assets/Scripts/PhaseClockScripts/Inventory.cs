using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject[] cell;
    public static int grid_id;
    public Collider2D item;
    public Sprite sprite;

    void Awake()
    {
        grid_id = -1;
        cell = new GameObject[transform.childCount];
        for (int j = 0; j < cell.Length; j++)
        {
            cell[j] = transform.GetChild(j).gameObject;
            cell[j].GetComponentInChildren<Item>().ID = j;
            cell[j].GetComponentInChildren<Item>().itemBool = false;
        }
    }

    public void ItemManager()
    {
        //cell = new GameObject[transform.childCount];
        for (int j = 0; j < cell.Length; j++)
        {
            if (cell[j].GetComponentInChildren<Item>().itemBool == false)
            {
                cell[j].GetComponentInChildren<Item>().loot = item;
                cell[j].GetComponentInChildren<Image>().sprite = sprite;
                cell[j].GetComponentInChildren<Item>().itemBool = true;
                break;
            }
        }
    }
}
