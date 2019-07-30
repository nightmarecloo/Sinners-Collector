using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Bot>().idleWalk = false;
            GetComponentInParent<Bot>().checkLook = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Bot>().idleWalk = false;
            GetComponentInParent<Bot>().checkLook = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Bot>().checkLook = false;
            GetComponentInParent<Bot>().GetWayPoints();
        }
    }
}
