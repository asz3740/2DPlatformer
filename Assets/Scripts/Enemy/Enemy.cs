using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Transform player;
    public Vector2 home;

    public float atkCooltime = 3;
    public float atkDelay;

    public bool facingLeft = false;
    public bool isFly = false;

    public int coin;

    public override void Start()
    {
        base.Start();
        //GetChild(1)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        home = transform.position;
    }

    void Update()
    {
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
    }

    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            if (facingLeft)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
                return;
            }
            transform.localScale = new Vector3(-1, 1, 1);
           
            facingRight = false;
        }
        else
        {
            if (facingLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
                return;
            }
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }         
    }


    public override void TakeDamage(int damaged)
    {
        HP -= damaged;
        print("적 맞음" + HP);

        base.TakeDamage(damaged);
        // 투명
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.color = Color.red;

        myAnim.SetTrigger("Damaged");
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        //spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.CoinSum(coin);
        GameManager.Instance.RandomItem(transform);
        Invoke("Respawn", 6f);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = home;
        myAnim.Play("Idle");
        HP = startHP;
    }




    




}
