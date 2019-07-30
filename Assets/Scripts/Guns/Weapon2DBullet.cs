using System.Collections;
using UnityEngine;

public class Weapon2DBullet : MonoBehaviour
{

    [SerializeField] private float damage = 15;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.isTrigger && coll.gameObject.tag == "Player")
        {
            PlayerHp target = coll.GetComponent<PlayerHp>();
            if (target != null) target.SetDamage(15);
            Destroy(gameObject);
        }
        if (!coll.isTrigger && coll.gameObject.tag == "Enemy")
        {
            BotHP target = coll.GetComponent<BotHP>();
            if (target != null) target.SetDamage(15);
            Destroy(gameObject);
        }
        if(coll.gameObject.tag == "Ground") Destroy(gameObject);
    }
}