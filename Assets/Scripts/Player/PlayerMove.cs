using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private Animator myAnim;
    private Collider2D col;


    [Header("이동")]

    [SerializeField]
    private float speed;

    [SerializeField]
    private float dashSpeed;

    private float walkAnimSpeed = 1.5f;
    private float runAnimSpeed = 2f;

    private bool facingRight = true;


    [Header("점프")]

    [SerializeField]
    private float jumpPower;

    private int jumpCount = 0;


    [Header("공격")]

    private float atkCoolTime;
    private float atkCheckCoolTime;
    private float atkCheckCool = 0.01f;


    [Header("기본 공격")]

    [SerializeField]
    private Transform MeleeAtkPos;

    [SerializeField]
    private GameObject MeleeAtkPrefab;

    private float MeleeAtkCool = 0.7f;


    [Header("원거리 공격")]

    [SerializeField]
    private Transform RangedAtkPos;

    [SerializeField]
    private GameObject RangedAtkPrefab;

    private float RangedAtkCool = 5f;


    [Header("상태")]
    private bool isAttack = false;
    private bool isSkill = false;


    [Header("참조")]
    //public GameManager gameManager;
  



    [Header("능력치")]
    public int HP = 100;
    public int MaxHP = 100;
    public int ATT = 15;



    public int MonsterCount;

    private SpriteRenderer spriteRenderer; // 피격 이펙트
    
    
    [Header("UI")]
    public Image CurImage;
    

    [Header("경험치")]
    public int EXP;
    public int MaxEXP = 120;
    public int LEVEL = 1;
    
    public Transform pos1;
    public Transform pos2;
    public Vector2 boxSize1;
    public Vector2 boxSize2;


   





    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        HP = MaxHP;
    }

    void Update()
    {
        Jump(); // 점프
        Skill(); // 스킬
        Attack(); // 공격
        //if(EXP >= MaxEXP)
        //{
        //    LEVEL++;
        //    EXP = EXP - MaxEXP;
        //    MaxEXP += 50;
        //    MaxHP += 10;
        //    if (HP <= MaxHP - 15)
        //    {
        //        HP += 15;
        //    }
        //    else
        //        HP = MaxHP;         
        //    ATT += 2;
        //}
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if(!isAttack)
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
        myAnim.SetFloat("MoveSpeed", walkAnimSpeed);
        MoveAnim();

        //SoundManager.instance.PlaySE("walk");
    }

    void FastMove(float horizontal)
    {
        myRigid.velocity = new Vector2(horizontal * dashSpeed, myRigid.velocity.y);
        myAnim.SetFloat("MoveSpeed", runAnimSpeed);
        MoveAnim();
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
        if ((horizontal > 0 && !facingRight && !isAttack) || (horizontal < 0 && facingRight && !isAttack))
        {
            ChangeDirection();
        }
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
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
            RaycastHit2D rayHit = Physics2D.Raycast(myRigid.position, Vector3.down, 2, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    myAnim.SetBool("isJumping", false);
                    jumpCount = 0;
                }
            }
               
        }
    }

    void Attack()
    {
        if (atkCoolTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                atkCheckCoolTime = 0f;
                myAnim.SetTrigger("Attack");
                isAttack = true;
                //SoundManager.instance.PlaySE("ATK");
                atkCoolTime = MeleeAtkCool;
            }
            atkCheckCoolTime += Time.deltaTime;
            if (atkCheckCoolTime > atkCheckCool)
            {
                isAttack = false;
            }
        }
        else
        {
            atkCoolTime -= Time.deltaTime;
        }
    }

    public void MeleeAttack()
    {
        Instantiate(MeleeAtkPrefab, MeleeAtkPos.position, Quaternion.identity);
    }

    void Skill()
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

    public virtual void ThrowObject()
    {
        if (facingRight)
        {
            GameObject temp = (GameObject)Instantiate(RangedAtkPrefab, RangedAtkPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.right);

        }
        else
        {
            GameObject temp = (GameObject)Instantiate(RangedAtkPrefab, RangedAtkPos.position, Quaternion.identity);
            temp.GetComponent<Shuriken>().Initialize(Vector2.left);
        }
    }

 
    //public override void TakeDamage()
    //{
    //    HP -= 10;
    //    GameManager.Instance.HPbar();
    //    print("플레이어 맞음"+HP);

    //    // 투명
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);

    //    myAnim.SetTrigger("Damaged");
    //    Invoke("OffDamaged", 1);
    //}

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    //public void Die()
    //{
    //    gameObject.SetActive(false);
    //    GameManager gameManager = FindObjectOfType<GameManager>();
    //    gameManager.EndGame();
    //}

    public void VelocityZero()
    {
        myRigid.velocity = Vector2.zero;
    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Coin")
    //    {
    //        bool isSilver = collision.gameObject.name.Contains("Silver");
    //        bool isGold = collision.gameObject.name.Contains("Gold");

    //        SoundManager.instance.PlaySE("coin");
    //        if (isSilver)
    //            gameManager.stageCoin += 10;
    //        else if (isGold)
    //            gameManager.stageCoin += 30;
    //        collision.gameObject.SetActive(false);
    //    }
    //    else if (collision.gameObject.tag == "Finish")
    //    {
    //        if (MonsterCount >= 4)
    //            gameManager.NextStage();
    //    }
    //    else if (collision.gameObject.tag == "Start")
    //    {
    //        SceneManager.LoadScene("Loading");
    //    }
    //    else if (collision.gameObject.tag == "Shop")
    //    {
    //        gameManager.NextStage();
    //    }
    //    else if (collision.gameObject.tag == "Boss")
    //    {
    //        OnDamaged(collision.transform.position);
    //        TakeDamage(15);
    //    }
    //    else if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        OnDamaged(collision.transform.position);
    //        TakeDamage(15);
    //    }
    //    else if (collision.gameObject.CompareTag("BigFire"))
    //    {
    //        OnDamaged(collision.transform.position);
    //        TakeDamage(30);
    //    }
    //}
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Monster1"))
    //    {
    //        OnDamaged(collision.transform.position);
    //        TakeDamage(15);
    //    }

    //    if (collision.gameObject.CompareTag("Monster2"))
    //    {
    //        OnDamaged(collision.transform.position);
    //        TakeDamage(15);
    //    }

    //}



}
