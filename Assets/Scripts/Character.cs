using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D myRigid;
    public Animator myAnim;
    public SpriteRenderer spriteRenderer; 
    public float speed;
    public int HP = 100;
    public int startHP;

    public bool facingRight = true;


    [Header("기본 공격")]

    [SerializeField]
    private Transform MeleeAtkPos;

    [SerializeField]
    private GameObject MeleeAtkPrefab;


    [SerializeField]
    private List<string> damageSources;

    [Header("원거리 공격")]

    [SerializeField]
    private Transform RangedAtkPos;

    [SerializeField]
    public GameObject RangedAtkPrefab;

    public virtual void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        startHP = HP;
    }

    public virtual void TakeDamage(int damaged)
    {
        print(HP); 
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public void MeleeAttack()
    {
        Instantiate(MeleeAtkPrefab, MeleeAtkPos.position, Quaternion.identity);
    }


    public virtual void ThrowObject()
    {
        if (facingRight)
        {
            GameObject temp = Instantiate(RangedAtkPrefab, RangedAtkPos.position, RangedAtkPrefab.transform.rotation);
            temp.GetComponent<Shuriken>().Initialize(Vector2.right);

        }
        else
        {
            GameObject temp = Instantiate(RangedAtkPrefab, RangedAtkPos.position, RangedAtkPrefab.transform.rotation);
            temp.GetComponent<Shuriken>().Initialize(Vector2.left);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //count++;
        if (damageSources.Contains(collision.tag))
        {
            print("맞음");
            Weapon weapon = collision.GetComponent<Weapon>();
            TakeDamage(weapon.damage);
        }
    }


}
