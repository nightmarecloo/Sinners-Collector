using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon2D : MonoBehaviour
{

    [Header("Настройки префаба:")]
    [SerializeField] private Rigidbody2D bulletPrefab; // если префаба пули нет, то оружие будет стрелять рейкастом
    [SerializeField] private float bulletSpeed = 5; // скорость префаба пули
    [Header("Общие параметры:")]
    [SerializeField] private Transform shootPoint; // точка оружия, откуда должны вылетать пули
    [SerializeField] private float fireRate = 1; // скорострельность
    [SerializeField] [Range(1, 5)] private int shootCount = 1; // выстрелов одновременно, например, дробовик может выстреливать больше одной пули
    [SerializeField] [Range(0.6f, 1f)] private float accuracy = 1; // разброс пуль, 1 = 100% точности
    [SerializeField] private int bulletCount = 15; // объем магазина
    [SerializeField] private float reloadTime = 2.5f; // время перезарядки в секундах
    [Header("Объект вращения:")]
    [SerializeField] private Transform zRotate; // объект вращения, например, само оружие
    [SerializeField] private float minAngle = -40; // ограничение по углам
    [SerializeField] private float maxAngle = 40;
    [Header("Родитель персонажа:")]
    [SerializeField] public Transform flipParent; // родитель для этого оружия (персонаж)
    [Header("Настройки рейкаста:")]
    [SerializeField] private float rayDistance = 100; // длинна луча
    [SerializeField] private LayerMask rayLayerMask; // маска цели
    [SerializeField] private float rayDamage = 15; // урон, наносимый цели
    public int bulletCountInt;
    public float invert, timeout, reloadTimeout;
    private Vector3 mouse, direction;
    private bool reload;
    public bool shoot;
    public bool flipGun;
    public SpriteRenderer sprite;
    public Transform gun;
    public Sprite imaGun;

    public Text bulletCoun;

    void Awake()
    {
        bulletCountInt = bulletCount;
        flipParent = null;
        shoot = false;
        sprite = GetComponentInChildren<SpriteRenderer>();
        flip = true;
    }

    IEnumerator WeaponReload()
    {
        // запуск процесса перезарядки
        reloadTimeout = 0;

        while (true)
        {
            yield return null;

            // процесс перезарядки
            reloadTimeout += Time.deltaTime;

            if (reloadTimeout > reloadTime)
            {
                // перезарядка завершена
                bulletCountInt = bulletCount;
                reload = false;
                break;
            }
        }
    }

    public bool flip;
    public void Flip()
    {
        //flip = flip ? false : true;


        if (invert == -1) sprite.flipX = true;
        if (invert == 1) sprite.flipX = false;
        //if (flipParent.GetComponent<Character>().sprite.flipX == true) sprite.flipX = true;
        //if (flipParent.GetComponent<Character>().sprite.flipX == false) sprite.flipX = false;




        //Vector3 theScale = flipParent.localScale;
        //theScale.x *= -1;
        //invert *= -1;
        //flipParent.localScale = theScale;
    }

    void LookAtMouse()
    {
        Vector3 mousePosMain = Input.mousePosition;
        mousePosMain.z = Camera.main.transform.position.z;
        mouse = Camera.main.ScreenToWorldPoint(mousePosMain);
        mouse.z = 0;
        Vector3 lookPos = zRotate.position;
        lookPos.z = 0;
        lookPos = mouse - lookPos;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x * invert) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        zRotate.rotation = Quaternion.AngleAxis(angle * invert, Vector3.forward);
    }

    void GetReload()
    {
        bulletCountInt = 0;
        reload = true;
        StartCoroutine(WeaponReload());
    }

    public void Update()
    {
        if (sprite.flipX)
        {
            shootPoint.transform.localPosition = new Vector3(-0.852f, 0.111f, 0);
        }
        else
        {
            shootPoint.transform.localPosition = new Vector3(0.852f, 0.111f, 0);
        }
    }

    void LateUpdate()
    {
        if (flipParent != null)
        {
            //invert = Mathf.Sign(flipParent.localScale.x); //возврат 1 или -1

            if (zRotate != null) LookAtMouse();

            //if (flipGun == true && mouse.x < flipParent.position.x && invert == 1)
            //{
            //    CameraFollow2D.faceLeft = true;
            //    Flip();
            //}
            //
            //else if (flipGun == true && mouse.x > flipParent.position.x && invert == -1)
            //{
            //    CameraFollow2D.faceLeft = false;
            //    Flip();
            //}
        }

        if (reload) return;

        if (shoot == true && flipParent != null) // выстрел ПКМ
        {
            if (bulletCountInt <= 0)
            {
                GetReload();
                return;
            }

            timeout += Time.deltaTime;
            if (timeout > fireRate)
            {
                timeout = 0;
                Shoot();
            }
        }
        else
        {
            timeout = Mathf.Infinity;
        }


        if (Input.GetKeyDown(KeyCode.R)) // перезарядка "R"
        {
            if (bulletCountInt != bulletCount) GetReload();
        }

        bulletCoun.text = bulletCountInt.ToString();
    }

    public void Reload()
    {
        if (bulletCountInt != bulletCount) GetReload();
    }

    void BulletShoot()
    {
        Rigidbody2D clone = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity) as Rigidbody2D;
        clone.velocity = direction * bulletSpeed;
        clone.transform.right = direction;
    }

    void RayShoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, rayDistance, rayLayerMask);

        if (hit.transform != null)
        {
            PlayerHp target = hit.transform.GetComponent<PlayerHp>();
            if (target != null) target.SetDamage(15);
        }
    }

    void Shoot()
    {
        for (int i = 0; i < shootCount; i++)
        {
            direction = (shootPoint.right + (Vector3)(Random.insideUnitCircle * (1f - accuracy))).normalized * invert;
            if (bulletPrefab == null) RayShoot(); else BulletShoot();
        }

        bulletCountInt--;
    }
}