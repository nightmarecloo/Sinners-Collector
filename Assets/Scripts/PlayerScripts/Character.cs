using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Character : CharacterPlayer
{
    private const float CheckPeriodText = 2f;
    private const float CheckPeriodFlip = 2f;
    private float m_LastCheck = CheckPeriodText;
    private float m_LastCheckFlip = CheckPeriodFlip;
   
    public Collider2D subject;
    public bool walk;
    public bool jump;
    public bool give;
    public GameObject gunReload;
    public GameObject imaGun;

    public Collider2D bot;

    public bool p1;
    public bool p2;
    public bool p3;

    public bool ladderClimbing;
    public Collider2D ladder;

    public Vector3 v3Velocity;
    public float velocitySpeed;


    public void Start()
    {
        hasLoot = true;
        flipCamera = true;
        CameraFollow2D.faceLeft = false;
        currentSpeed = 1;
        v3Velocity = rigidbody.velocity;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Loot" && GetComponentInChildren<Take>().hasLoot == true)
        {
            subject = collision;
        }
        if (collision.gameObject.tag == "Ladder")
        {
            ladder = collision;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MyLoot")
        {
            subject = collision;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (GetComponentInChildren<Take>().hasLoot == true) subject = null;
        if (collision.gameObject.tag == "Ladder")
        {
            //rigidbody.velocity = new Vector3(0, 0, 0);
            ladderClimbing = false;
            ladder = null;
        }
    }

    public void Punch3()
    {
        p3 = false;
        punchController.GetComponent<BoxCollider2D>().size = new Vector2(4.11f, 0.55f);
        punchController.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.28f);
        punchController.gameObject.GetComponent<Punch>().Punch3();
    }

    public void Punch2()
    {
        p2 = false;
        punchController.GetComponent<BoxCollider2D>().size = new Vector2(5.03f, 0.55f);
        punchController.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        punchController.gameObject.GetComponent<Punch>().Punch2();
    }

    public void Punch1()
    {
        p1 = false;
        punchController.GetComponent<BoxCollider2D>().size = new Vector2(1.7f, 1.03f);
        punchController.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.3f);
        punchController.gameObject.GetComponent<Punch>().Punch1();
    }

    public GameObject inventory;
    
    public void ButtonTakeEnter()
    {
        if (subject != null)
        {      
            give = give ? false : true;
            
            if (give == true && takeCheck.GetComponent<Take>().take == false) //беру предмет
            {
                inventory.GetComponent<Inventory>().item = subject;
                inventory.GetComponent<Inventory>().sprite = subject.GetComponent<Weapon2D>().imaGun; //передаю спрайт предмета в скрипт Inventory
                inventory.GetComponent<Inventory>().ItemManager();

                GetComponent<ObjectNameView>().enabled = true;

                takeCheck.GetComponent<Take>().TakeSubject();

                hasLoot = false;
                if (takeCheck.GetComponent<Take>().take == false)
                {
                    takeCheck.GetComponent<Take>().take = true;
                }

                if (takeCheck.GetComponent<Take>().take == true)
                {
                    if (sprite.flipX == true) subject.GetComponent<Weapon2D>().invert = -1;
                    if (sprite.flipX == false) subject.GetComponent<Weapon2D>().invert = 1;
                    subject.GetComponent<Weapon2D>().Flip();
                    FlipLoot();
                }
            }
        } 
    }

    public void ButtonTakeExit()
    {
        give = give ? false : true;

        if (give == false && takeCheck.GetComponent<Take>().take == true) //выкидываю предмет, плевать на слой в проверке
        {
            takeCheck.GetComponent<Take>().UnTake();
            hasLoot = true;
            if (takeCheck.GetComponent<Take>().take == true)
            {
                takeCheck.GetComponent<Take>().take = false;
            }
        }
    }

    public void GunReload()
    {
        if (give == true)
        {
            subject.GetComponent<Weapon2D>().Reload();
        }
    }


    public bool ladderState;

    public void PlayerJump()
    {
        if (isGrounded && block == false && ladderClimbing == false)
        {
            jump = true;
            Jump();
        }
        if (ladder != null)
        {
            ladderState = true;
            ladderClimbing = true;
            move = 1;
        }
    }

    public void PlayerJumpRight()
    {
        if (flipPlayer == true && block == false)
        {
            CameraFollow2D.faceLeft = false;
            if (give == false)
            {
                invert = 1;
                Flip();
            }
            if (give == true)
            {
                subject.GetComponent<Weapon2D>().invert = 1;
                Flip();
                subject.GetComponent<Weapon2D>().Flip();
                FlipLoot();
            }
        }
        move = 1;
        if (isGrounded && block == false && ladder == null)
        {
            jump = true;
            JumpRight();
        }
    }

    public void PlayerJumpLeft()
    {
        if (flipPlayer == false && block == false)
        {
            CameraFollow2D.faceLeft = true;
            if (give == false)
            {
                invert = -1;
                Flip();
            }
            if (give == true)
            {
                subject.GetComponent<Weapon2D>().invert = -1;
                Flip();
                subject.GetComponent<Weapon2D>().Flip();
                FlipLoot();
            }
        }
        move = -1;
        if (isGrounded && block == false && ladder == null)
        {
            jump = true;
            JumpLeft();
        }
    }

    public void PlayerSetDown()
    {
        if (ladder != null)
        {
            ladderClimbing = true;
            move = -1;
        }
    }



    public void ButtonEnterRight()
    {
        if (flipPlayer == true && block == false)
        {
            CameraFollow2D.faceLeft = false;
            if (give == false)
            {
                invert = 1;
                Flip();
            }
            if (give == true)
            {
                subject.GetComponent<Weapon2D>().invert = 1;
                Flip();
                subject.GetComponent<Weapon2D>().Flip();
                FlipLoot();
            }
        }
        walk = true;
        move = 1;
    }

    public void ButtonEnterLeft()
    {
        if (flipPlayer == false && block == false)
        {
            CameraFollow2D.faceLeft = true;
            if (give == false)
            {
                invert = -1;
                Flip();
            }
            if (give == true)
            {
                subject.GetComponent<Weapon2D>().invert = -1;
                Flip();
                subject.GetComponent<Weapon2D>().Flip();
                FlipLoot();
            }
        }
        walk = true;
        move = -1;
    }

    public void ButtonDirrectionExit()
    {
        if (!isGrounded) rigidbody.velocity = new Vector3(0, 0, 0);
        walk = false;
        move = 0;
    }

    public void ButtonJumpExit()
    {
        if (ladderClimbing == true)
        {
            ladderClimbing = false;
        }
        jump = false;
    }
















    

    private void Update()
    {
        anim.SetBool("walk", walk);
        anim.SetBool("p1", p1);
        anim.SetBool("p2", p2);
        anim.SetBool("p3", p3);
        anim.SetBool("jump", jump);
        anim.SetBool("block", block);


        velocitySpeed = rigidbody.velocity.magnitude;
        //Debug.Log(velocitySpeed); //текущаяя скорость персонажа



        if (ladder != null) //если есть касание с лестницей, то физика игрока становится Dynamic и он обезддвиживается обнулением velocity
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.velocity = new Vector3(0, 0, 0);
        }

        if (ladderClimbing == true) //взбирание по лестнице вызовом соответсвующщего метода
        {
            LadderClimbing();
        }

        if (walk == true && currentSpeed < speed) //покадровое ускорение бега после остановки для плавности передвижения
        {
            currentSpeed += 1f;
        }

        if (walk == false) //если игрок остановился, то его начальная скорость бега уменьшается
        {
            currentSpeed = 5f;
        }
        
        if (!isGrounded)
        {
            currentSpeed = 15.0f;
        }

        if (ladder != null) rigidbody.gravityScale = 0;
        else rigidbody.gravityScale = 5;

        if (subject != null)
        {
            
            imaGun.SetActive(true);
            m_LastCheck -= Time.deltaTime;
            if (m_LastCheck < 0)
            {
                GetComponent<ObjectNameView>().enabled = false;
                m_LastCheck = CheckPeriodText;
            }
        }
        else
        {
            imaGun.SetActive(false);
        }





        //if (Input.GetButton("Horizontal")) Run();

        //if (isGrounded && Input.GetButtonDown("Jump"))
        //{
        //    state = CharState.Jump;
        //    Jump();
        //}

        //if (isPunched && Input.GetButtonDown("Punch")) 
        //{
        //    punchCheck.GetComponent<Punch>().Punch1(); 
        //}

        

        if (walk == true && block == false) //если зажата кнопка бега воспроизводится анимация бега
        {
            Run();
        }
        //if (walk == false && punch == false && block == false) state = CharState.Idle;  //если кнопка бега не зажата воспроизводится анимация бездействия
        //if (jump == true && !isGrounded) state = CharState.Jump;
    }

    public void StartPunch()
    {
        p1 = false;
        p2 = false;
        p3 = false;
    }

    public void EndPunch()
    {
        punch = false;
    }

    public void EndAnim()
    {
    }

    public bool punch;
    public bool block;
}