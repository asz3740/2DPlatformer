using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shuriken : Weapon
{
    [SerializeField]
    private float speed;
    private Rigidbody2D myRigid;
    private Vector2 direction;

    //[SerializeField]
    //private string targetTag;

    [SerializeField]
    private bool isRotate;


    //private Enemy enemy;

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        //enemy = GameObject.Find("Monster").GetComponent<Enemy>();
    }

    void Update()
    {
        if(isRotate)
        {
            transform.Rotate(Vector3.forward, 200 * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {

        myRigid.velocity = direction * speed;

    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == targetTag)
    //    {
    //        print("수리검");
    //    }
    //}
}
