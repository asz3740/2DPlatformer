using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Character
{

    Vector3 appearPos = new Vector3(10f, 4.5f, 0);
    Vector3 shurikenPos = new Vector3(0, 0, 0);
    private float startCoolTime;
    private float skillCoolTime;
    public GameObject fire;
    public GameObject shuriken;
    int count;

    public bool pattern5;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Player player;
    public Transform pos4;
    public Vector2 boxSize;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 4);
    }

    void Update()
    {
        startCoolTime += Time.deltaTime;
        if (startCoolTime <= 1.5f)
            Appear();

    }

    void Appear()
    {
        transform.position = Vector3.MoveTowards(transform.position, appearPos, 0.5f);
    }

    void Think()
    {
        count = 0;
        print("patternIndex" + patternIndex);
        patternIndex = patternIndex == 5 ? 1 : patternIndex + 1;
        switch (patternIndex)
        {
            case 1:
                Fire1();
                Fire2();
                break;
            case 2:
                Dash();
                break;
            case 3:
                GameObject shur = Instantiate(shuriken, shurikenPos, transform.rotation);
                Destroy(shur, 12f);
                Invoke("Think", 4f);
                break;
            case 4:
                Fire1();
                Fire2();
                ChangePos();
                break;
            case 5:
                pattern5 = true;
                myAnim.SetBool("Skill1", true);
                gameObject.layer = 16;
                Invoke("Think", 4f);
                break;
        }

    }

    int roundNum1;
    int roundNum2;
    public Transform[] Pos;
    void Fire1()
    {
        roundNum1++;
        myAnim.SetTrigger("Attack");
        int ran1 = Random.Range(0, 4);
        for (int index = 0; index < roundNum1; index++)
        {
            GameObject bullet = Instantiate(fire, Pos[ran1].position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum1),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum1));
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum1 + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
            Destroy(bullet, 2f);
        }

        if (roundNum1 <= 10)
        {
            Invoke("Fire1", 1.5f);
        }
        else
        {
            roundNum1 = 0;
            Invoke("Think", 4f);
        }
    }

    void Fire2()
    {
        roundNum2++;
        myAnim.SetTrigger("Attack");
        int ran2 = Random.Range(0, 4);
        for (int index = 0; index < roundNum2; index++)
        {
            GameObject bullet = Instantiate(fire, Pos[ran2].position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum2),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum2));
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum2 + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
            Destroy(bullet, 2f);
        }

        if (roundNum2 <= 10)
        {
            roundNum2 = 0;
            Invoke("Fire2", 1f);
        }
    }

    void Dash()
    {
        count++;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        myAnim.SetTrigger("Attack");
        print(count);
        if (count < 4)
            Invoke("Dash", 1.5f);
        else
        {
            transform.position = appearPos;
            Invoke("Think", 4f);
        }

    }

    void ChangePos()
    {
        print("ChangePos");
        count++;
        float ranX = Random.Range(-15, 16);
        float ranY = Random.Range(-12, 8);

        transform.position = new Vector3(ranX, ranY, transform.position.z);

        if (count < 6)
            Invoke("ChangePos", 1.5f);

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("BossSkill") && collision.gameObject.tag =="Player")
        {
            print("닺");
            player.TakeDamage(10);
        }
    }

    public override void TakeDamage(int damaged)
    {
        print("damaged" + damaged);
        HP -= damaged;
        print("보스 맞음" + HP);
        base.TakeDamage(damaged);
        // 투명
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.color = Color.red;

        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.SuccessGame();
    }
}
