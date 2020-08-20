using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [Header("이동")]

    [SerializeField]
    private float dashSpeed;

    private float walkAnimSpeed = 1.5f;
    private float runAnimSpeed = 2f;




    [Header("점프")]

    [SerializeField]
    private float jumpPower;

    private int jumpCount = 0;


    [Header("공격")]

    private float atkCoolTime;
    private float atkCheckCoolTime;
    private float atkCheckCool = 0.3f;



    private float MeleeAtkCool = 1.7f;





    public float RangedAtkCool = 4f;


    [Header("상태")]
    private bool isAttack = false;
    public bool isSwimming = false;
    private bool isSkill = false;



    [Header("UI")]
    public Image CurImage;


    [Header("경험치")]
    public int EXP;
    public int MaxEXP = 120;
    public int LEVEL = 1;


    public Inventory inventoryPrefab;

    Inventory inventory;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = Instantiate(inventoryPrefab);
    }

    void Update()
    {
        if (transform.position.y <= -20f)
        {
            Die();
        }

        if(!isSwimming)
        {
            Jump(); // 점프
            RangedAttack(); // 스킬
            Attack(); // 공격
        }

        UseItem();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (!isAttack)
        {
            Flip(horizontal);
            if (Input.GetKey(KeyCode.Q))
            {
                FastMove(horizontal);
            }
            else
            {
                Move(horizontal);
            }
        }



    }

    void Move(float horizontal)
    {
        myRigid.velocity = new Vector2(horizontal * speed, myRigid.velocity.y);
        if(isSwimming)
        {
            myAnim.SetBool("isSwimming", true);
            return;
        }

        print("return");
        myAnim.SetFloat("MoveSpeed", walkAnimSpeed);
        MoveAnim();

        //SoundManager.instance.PlaySE("walk");
    }

    void FastMove(float horizontal)
    {
        if (!isSwimming)
        {
            myRigid.velocity = new Vector2(horizontal * dashSpeed, myRigid.velocity.y);
            myAnim.SetFloat("MoveSpeed", runAnimSpeed);
            MoveAnim();
        }
       
    }

    void MoveAnim()
    {
        if (Mathf.Abs(myRigid.velocity.x) < 0.1)
        {
            myAnim.SetBool("isWalking", false);
        }

        else
        {
            myAnim.SetBool("isWalking", true);
        }
    }

    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            ChangeDirection();
        }
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1.7f, 1);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount <= 1)
        {
            //SoundManager.instance.PlaySE("jump");
            // 점프 
            myRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            myAnim.SetBool("isJumping", true);
            jumpCount++;
        }

        if (myRigid.velocity.y < 0)
        {
            print("true");
            Debug.DrawRay(myRigid.position, Vector3.down * 3, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, Vector3.down,3, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 2f)
                {
                    myAnim.SetBool("isJumping", false);
                    jumpCount = 0;
                }
            }

        }
    }

    void Attack()
    {
        print(isAttack);
        if (atkCoolTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("Attack");
                atkCoolTime = MeleeAtkCool;
                isAttack = true;
                atkCheckCoolTime = 0f;
                //SoundManager.instance.PlaySE("ATK");
            }          
        }
        else
        {
            atkCoolTime -= Time.deltaTime;

        }
        atkCheckCoolTime += Time.deltaTime;

        if (atkCheckCoolTime >= atkCheckCool)
        {
            isAttack = false;
        }
    }

  

    void RangedAttack()
    {
        if (atkCoolTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //CurImage.fillAmount = 1;
                //StartCoroutine("Cooltime");
                atkCheckCoolTime = 0f;
                //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
                myAnim.SetTrigger("Skill");
                isSkill = true;
                //SoundManager.instance.PlaySE("candy");
                atkCoolTime = RangedAtkCool;
            }
            atkCheckCoolTime += Time.deltaTime;
            if (atkCheckCoolTime > atkCheckCool)
            {
                isSkill = false;
            }
        }
        else
        {
            atkCoolTime -= Time.deltaTime;
        }
    }


    public override void TakeDamage(int damaged)
    {
        HP -= damaged;

        print("플레이어 맞음" + HP);
        base.TakeDamage(damaged);
        // 투명
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        myAnim.SetTrigger("Damaged");
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public override void Die()
    {
        base.Die();
        GameManager.Instance.EndGame();
    }

    public void VelocityZero()
    {
        myRigid.velocity = Vector2.zero;
    }
   

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.NextStage();
        }
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
        }


        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if(hitObject != null)
            {
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:

                        ItemManager.Instance.ItemCoin();
                        shouldDisappear = true;

                        break;
                    case Item.ItemType.POTION:
                        print(hitObject);
                        shouldDisappear = inventory.AddItem(hitObject);
                        shouldDisappear = true;

                        break;
                    default:
                        shouldDisappear = inventory.AddItem(hitObject);
                        shouldDisappear = true;
                        break;
                }
                if(shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
                print("hit: " + hitObject.objectName);
            
            }
        }

    }

    public void UseItem()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.UseItem(1);
        }
    }

    IEnumerator HPHeal()
    {
        while (true)
        {
            if (HP == startHP)
                HP = startHP;
            else
                 HP += 1;
            
            yield return new WaitForSeconds(1);
        }
    }




}
