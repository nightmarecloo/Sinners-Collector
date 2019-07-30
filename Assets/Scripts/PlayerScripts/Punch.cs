using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    public Collider2D enemy;
    public Collider2D player;

    public void Punch1()
    {
        CameraShake.Shake(0.1f, 0.3f);
        if (enemy != null)
        {
            enemy.GetComponent<BotHP>().SetDamage(15);
        }

        if (player != null)
        {
            player.GetComponent<PlayerHp>().SetDamage(15);
        }
    }

    public void Punch2()
    {
        CameraShake.Shake(0.2f, 0.4f);
        if (enemy != null)
        {
            enemy.GetComponent<BotHP>().SetDamage(25);
        }
    }

    public void Punch3()
    {
        CameraShake.Shake(0.2f, 0.4f);
        if (enemy != null)
        {
            enemy.GetComponent<BotHP>().SetDamage(50);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Enemy") enemy = collision;
        if (collision.gameObject.tag == "Player") player = collision;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") enemy = collision;
        if (collision.gameObject.tag == "Player") player = collision;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") enemy = null;
        player = null;
    }
}
