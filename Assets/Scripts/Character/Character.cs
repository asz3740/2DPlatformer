using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [System.NonSerialized]
    public Rigidbody2D myRigid;
    [System.NonSerialized]
    public Animator myAnim;
    [System.NonSerialized]
    public SpriteRenderer spriteRenderer;

    [Header("이동")]
    public float speed;
    public bool facingRight = true;
    
    public int HP = 100;
    public int startHP;

    [SerializeField]
    private List<string> damageSources;

    [Header("근접 공")]

    [SerializeField]
    private Transform MeleeAtkPos;
    [SerializeField]
    private GameObject MeleeAtkPrefab;

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
        if (damageSources.Contains(collision.tag))
        {
            Weapon weapon = collision.GetComponent<Weapon>();
            TakeDamage(weapon.damage);
        }
    }


}
