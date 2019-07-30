using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour {

    public Animator anim;
    [SerializeField]
    public float speed;
    public float currentSpeed;
    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    public float jumpForce;

    new public Rigidbody2D rigidbody;
    private Animator animator;
    public SpriteRenderer sprite;

    public GameObject player;
    GameObject[] enemies;

    public bool hasLoot;

    [Header("Касание с землей:")]
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundRadius = 0.01f;
    [SerializeField] public LayerMask whatIsGround;
    [Header("Удары:")]
    [SerializeField] public bool isPunched = false;           
    [SerializeField] public Transform punchCheck;             
    [SerializeField] public float punchRadius = 0.2f;         
    [SerializeField] public LayerMask whatIsPunch;
    [Header("Касание с предметом:")]
    [SerializeField] public bool isTake = false;              
    [SerializeField] public Transform takeCheck;              
    [SerializeField] public float takeRadius = 2.0f;
    [SerializeField] public LayerMask whatIsTake;
    [Header("Касание со стеной:")]
    [SerializeField] public bool isWall = false;              
    [SerializeField] public Transform wallCheck;              
    [SerializeField] public float wallRadius = 2.0f;
    [SerializeField] public LayerMask whatIswall;
    

    //public CharState state
    //{
    //    get { return (CharState)animator.GetInteger("State"); }
    //    set { animator.SetInteger("State", (int)value); }
    //}
    
   

    public void Awake()
    {
        speed = 20.0f;
        maxSpeed = 30.0f;
        jumpForce = 17.0f;
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);   
        
        isPunched = Physics2D.OverlapCircle(punchCheck.position, punchRadius, whatIsPunch);    
        
        isTake = Physics2D.OverlapCircle(takeCheck.position, takeRadius, whatIsTake); 
        
        isWall = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIswall);   
    }
    
    public float move;

    public void Run()
    {

        //Vector3 direction = transform.right * move;

        //transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * currentSpeed, GetComponent<Rigidbody2D>().velocity.y);
        //rigidbody.AddForce(transform.right * move * speed / Time.deltaTime);
    }




    public GameObject punchController;
    public GameObject takeController;

    public void FlipLoot()
    {

        if (takeCheck != null)
        {
            takeCheck.GetComponent<Take>().subject.transform.localPosition = new Vector3(0, -0.08f, 0);
        }

        

       //if (sprite.flipX)
       //{
       //    if (takeCheck != null)
       //    {
       //        takeCheck.GetComponent<Take>().subject.transform.localPosition = new Vector3(-0.35f, 0.15f, 0);
       //    }
       //}
       //else
       //{
       //    if (takeCheck != null)
       //    {
       //        takeCheck.GetComponent<Take>().subject.transform.localPosition = new Vector3(0.35f, 0.15f, 0);
       //    }
       //}
    }

    public void LadderClimbing()
    {
        //rigidbody.bodyType = RigidbodyType2D.Kinematic;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, move * speed);
    }

    public void Jump()
    {
        float x = rigidbody.velocity.x;
        float y = rigidbody.velocity.y;
        if (x != 0)
        {
            rigidbody.velocity = new Vector2(x * 0, 22);
        }

        if (x == 0)
        {
            rigidbody.AddForce(new Vector3(0, 17, 0), ForceMode2D.Impulse);
        }
        //rigidbody.AddForce(transform.up * rigidbody.mass * jumpForce, ForceMode2D.Impulse);
    }

    public int newVelocityValue = 20;
    public void JumpRight()
    {
        float x = rigidbody.velocity.x;
        float y = rigidbody.velocity.y;
        if (y == 0)
        {
            rigidbody.AddForce(new Vector3(10, 17, 0), ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.velocity = new Vector2(newVelocityValue - 5, newVelocityValue);
        }
    }

    public void JumpLeft()
    {
        float x = rigidbody.velocity.x;
        float y = rigidbody.velocity.y;
        if (y == 0)
        {
            rigidbody.AddForce(new Vector3(-10, 17, 0), ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.velocity = new Vector2(-newVelocityValue - 5, newVelocityValue);
        }
    }
    
    /// <summary>
    /// //////////////////////////////////////////////////////////////////
    /// </summary>

    [Header("Флипы персонажа:")]
    [SerializeField] public Transform flipParent;
    [SerializeField] private Transform zRotate;
    [SerializeField] private float minAngle = -40; // ограничение по углам
    [SerializeField] private float maxAngle = 40;

    public float invert;
    public Vector3 mouse;
    public bool flipPlayer;

    public void Flip()
    {
        flipPlayer = flipPlayer ? false : true;

        if (flipPlayer == true)
        {
            punchController.transform.localPosition = new Vector3(-0.524f, 0.483f, 0);
            sprite.flipX = true;
        }
        if (flipPlayer == false)
        {
            punchController.transform.localPosition = new Vector3(0.524f, 0.483f, 0);
            sprite.flipX = false;
        }
        //theScale = flipParent.localScale;
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

    public bool flipCamera;
    public void LateUpdate()
    {
        invert = Mathf.Sign(flipParent.localScale.x); //возврат 1 или -1

        if (zRotate != null) LookAtMouse();

       //if (flipCamera == true && mouse.x < flipParent.position.x && invert == 1 && hasLoot == true)
       //{
       //    CameraFollow2D.faceLeft = true;
       //    Flip();
       //}
       //else if (flipCamera == true && mouse.x > flipParent.position.x && invert == -1 && hasLoot == true)
       //{
       //    CameraFollow2D.faceLeft = false;
       //    Flip();
       //}
    }










    
}

//public enum CharState
//{
//    Idle,
//    Run,
//    Jump,
//    Punch1,
//    Punch2,
//    Punch3,
//    Block
//
//}
