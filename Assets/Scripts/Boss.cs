using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Character
{

    Vector3 appearPos = new Vector3(-4f, 1.5f, 0);

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject bulletC;
    int count;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public GameObject player;
    public Transform pos4;
    public Vector2 boxSize;
    //private Vector3 target;

    public override void Start()
    {
        //target = FindObjectOfType<PlayerMove>().transform;

        Invoke("Think", 4);
    }

    void Update()
    {
        Appear();
    }
    void Appear()
    {
        transform.position = Vector3.MoveTowards(transform.position, appearPos, 0.5f);
    }
    void Think()
    {
        patternIndex = patternIndex == 4 ? 0 : patternIndex + 1;
        curPatternCount = 0;
        count = 0;

        switch (patternIndex)
        {
            case 0:
                Fire1();
                Fire1_1();
                break;
            case 1:
                Dash();
                break;
            case 2:
                Fire2();       
                break;
            case 3:
                Bress();
                break;
            case 4:
                BigFire();
                break;
        }         
    }

    void Fire1()
    {
        myAnim.SetTrigger("Attack");

        int roundNum = count + 1;
        for (int index = 0; index < roundNum; index++)
        {

            GameObject bullet = Instantiate(bulletA, pos1.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
            Destroy(bullet, 3);
        }

        for (int index = 0; index < roundNum; index++)
        {

            GameObject bullet = Instantiate(bulletA, pos2.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum)); 
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
            Destroy(bullet, 3);
        }

        count += 1;
        curPatternCount++;
        if(curPatternCount < maxPatternCount[patternIndex])
            Invoke("Fire1", 4f);
        else
        Invoke("Think", 4);
    }

    void Fire1_1()
    {
        int roundNum = count + 1;
        for (int index = 0; index < roundNum; index++)
        {     
            GameObject bullet = Instantiate(bulletA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum)); //player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
            Destroy(bullet, 3);
        }
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("Fire1_1", 3f);
    }

    void Fire2()
    {
        //SoundManager.instance.PlaySE("cang");
        myAnim.SetTrigger("Attack");
        GameObject bullet = Instantiate(bulletB, pos3.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]),-1);
        rigid.AddForce(dirVec.normalized * 15, ForceMode2D.Impulse);
        Destroy(bullet, 3);
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("Fire2", 0.2f);
        else
            Invoke("Think", 4);
    }

    void Dash()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        myAnim.SetTrigger("Skill2");
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("Dash", 1f);
        else
            Invoke("Think", 4);
    }

    void Bress()
    {
        //Vector3 temp = new Vector3(-5, 0, 0);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        myAnim.SetTrigger("Skill1");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos4.position, boxSize, 0);
        foreach (Collider2D i in collider2Ds)
        {
            if (i.tag == "Player")
            {
                //i.GetComponent<PlayerMove>().TakeDamage(15);
                //i.GetComponent<PlayerMove>().OnDamaged(i.transform.position);
            }
        }
            curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("Bress", 1f);
        else
            Invoke("Think", 4);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos4.position, boxSize);
    }

    void BigFire()
    {
        SoundManager.instance.PlaySE("cang");
        myAnim.SetTrigger("Attack");
        GameObject bullet = Instantiate(bulletC, pos3.position, transform.rotation);    
        Destroy(bullet, 4);
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("Fire2", 2f);
        else
            Invoke("Think", 4);
    }


    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }



    public void TakeDamage(int damage)
    {

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 1);
        HP = HP - damage;
        if (HP <= 0)
        {
            myAnim.SetTrigger("Died");
            Invoke("Die", 3f);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("Boss_After");
        gameObject.SetActive(false);
    }

}
