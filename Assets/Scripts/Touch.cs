using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x))
        {
            if (eventData.delta.y > 0)
            {
                if(GameObject.Find("Gun").GetComponent<Weapon2D>().shoot == false) 
                {
                    //GameObject.Find("PlayerOne").GetComponent<Character>().PlayerJump();
                }
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
}
