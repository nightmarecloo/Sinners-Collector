using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header("Задержка времни:")]
    [SerializeField] public const float CheckPeriod = 2f;
    public float m_LastCheck = CheckPeriod;     // переменные для задержки действия

    [SerializeField]
    public float jumpForce = 10.0F;
    public Collider2D player;

    new public Rigidbody2D rigidbody;
    private Animator animator;
    public SpriteRenderer sprite;
    

    public bool hasLoot;

    public float speed = 10f;
    public GameObject playerOne;
    
    public float positionPlayer;
    public float positionBot;
    public float position;
    public static bool walk;

    public ParticleSystem punch1Layer1;
    public ParticleSystem punch1Layer2;
    public ParticleSystem punch1Layer3;
    public ParticleSystem punch1Layer4;

    [Header("Касание с землей:")]
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundRadius = 0.01f;
    [SerializeField] public LayerMask whatIsGround;
    [Header("Удары:")]
    [SerializeField] public bool isPunched = false;
    [SerializeField] public Transform punchCheck;
    [SerializeField] public float punchRadius = 0.4f;
    [SerializeField] public LayerMask whatIsPunch;
    [Header("Дочерние объекты:")]
    public GameObject punchObject;
    public GameObject ground;
    public GameObject look;

    [SerializeField] public Transform flipParent;

    public CharState2 state
    {
        get { return (CharState2)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    public void Awake()
    {
        walk = true;
        playerOne = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Start()
    {
        punch1Layer1.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
        punch1Layer2.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
        punch1Layer3.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
        punch1Layer4.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
        punch1Layer1.Stop();
        punch1Layer2.Stop();
        punch1Layer3.Stop();
        punch1Layer4.Stop();
        hasLoot = true;
        flipBot = false;
        wayPos = 1;
        GetWayPoints();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        isPunched = Physics2D.OverlapCircle(punchCheck.position, punchRadius, whatIsPunch);
    }

    public GameObject WayA;
    public float a;
    public GameObject WayB;
    public float b;
    public bool idleWalk;

    public void GetWayPoints()
    {
        a = WayA.transform.position.x;
        b = WayB.transform.position.x;
        idleWalk = true;
    }

    public GameObject bot;
    public bool checkLook;

    public void Punch1()
    {
        playerOne.GetComponent<PlayerHp>().bot = bot;
        punch1Layer1.Stop();
        punch1Layer2.Stop();
        punch1Layer3.Stop();
        punch1Layer4.Stop();
        punchObject.gameObject.GetComponent<Punch>().Punch1();
    }

    public void StartPunch()
    {
        if (checkLook == true)
        {
            punch1Layer1.Play();
            punch1Layer2.Play();
            punch1Layer3.Play();
            punch1Layer4.Play();
        }
    }

    public void MidPunch()
    {
    }

    public void EndPunch()
    {
        speed = 10;
        punch = false;
        if (idleWalk == true) state = CharState2.Run;
        //idleWalk = true;
        if (playerOne.GetComponent<PlayerHp>().currentHp <= 0)
        {
            speed = 0;
            state = CharState2.Idle;
        }
    }

    public float wayPos;
    public bool punch;

    public void Update()
    {
        if (isPunched && punch == false)
        {
            speed = 0;
            state = CharState2.Punch;
            //checkLook = false;
            walk = false;
            punch = true;
        }

        if (idleWalk == true)  //бег между точками
        {
            if (bot.transform.position.x >= b)
            {
                speed = 0;
                m_LastCheck -= Time.deltaTime;
                if (m_LastCheck > 0)
                {
                    state = CharState2.Idle;
                }
                else
                {
                    m_LastCheck = CheckPeriod;
                    speed = 10;
                    state = CharState2.Run;
                    wayPos = -1;
                }
            }
            if (bot.transform.position.x <= a)
            {
                speed = 0;
                m_LastCheck -= Time.deltaTime;
                if (m_LastCheck > 0)
                {
                    state = CharState2.Idle;
                }
                else
                {
                    m_LastCheck = CheckPeriod;
                    speed = 10;
                    state = CharState2.Run;
                    wayPos = 1;
                }
            }
            if (speed == 10)
            {
                Vector3 direction = transform.right * wayPos;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            }
            
        }

        if (checkLook == true && punch == false)  //бег за игроком
        {
            speed = 10;
            if (speed == 10) state = CharState2.Run;
            positionBot = bot.transform.position.x;
            if (playerOne != null) positionPlayer = playerOne.transform.position.x;
            position = (positionBot > positionPlayer) ? -1 : 1;

            Vector3 direction = transform.right * position;
            if (playerOne != null) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }


        if (sprite.flipX)
        {
            punchObject.transform.localPosition = new Vector3(-0.237f, 0.166f, 0);
        }
        else
        {
            punchObject.transform.localPosition = new Vector3(0.237f, 0.166f, 0);
        }


        if (flipBot == false && position == -1 && idleWalk == false)
        {
            Flip();
        }

        if (flipBot == false && wayPos == -1 && idleWalk == true)
        {
            Flip();
        }

        if (flipBot == true && position == 1 && idleWalk == false)
        {
            Flip();
        }

        if (flipBot == true && wayPos == 1 && idleWalk == true)
        {
            Flip();
        }
    }

    public bool flipBot;

    public void Flip()
    {
        flipBot = flipBot ? false : true;

        if (flipBot == true)
        {
            punch1Layer1.transform.localPosition = new Vector3(0.288f, 0.066f, 0);
            punch1Layer2.transform.localPosition = new Vector3(0.288f, 0.066f, 0);
            punch1Layer3.transform.localPosition = new Vector3(0.288f, 0.066f, 0);
            punch1Layer4.transform.localPosition = new Vector3(0.288f, 0.066f, 0);
            look.GetComponent<Collider2D>().offset = new Vector2(-1.32f, 0f);
            sprite.flipX = true;
        }
        if (flipBot == false)
        {
            punch1Layer1.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
            punch1Layer2.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
            punch1Layer3.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
            punch1Layer4.transform.localPosition = new Vector3(-0.288f, 0.066f, 0);
            look.GetComponent<Collider2D>().offset = new Vector2(1.32f, 0f);
            sprite.flipX = false;
        }
        
        //Vector3 theScale = transform.localScale;
        ////зеркально отражаем персонажа по оси Х
        //theScale.x *= -1;
        ////задаем новый размер персонажа, равный старому, но зеркально отраженный
        //transform.localScale = theScale;
    }

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") player = collision;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }

    public GameObject evanDrone;

    public void OnBecameVisible()
    {
       // evanDrone.GetComponentInChildren<Weapon2D>().enemyLook = true;
    }
    public void OnBecameInvisible()
    {
       // evanDrone.GetComponentInChildren<Weapon2D>().enemyLook = false;
    }
}

public enum CharState2
{
    Idle,
    Run,
    Punch
}