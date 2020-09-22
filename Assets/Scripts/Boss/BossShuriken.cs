using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShuriken : MonoBehaviour
{
    private Rigidbody2D myRigid;

    private float startCoolTime;

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward, 400 * Time.deltaTime);
        startCoolTime += Time.deltaTime;

        if (startCoolTime <= 0.8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 0), 0.5f);
        }
        else if (startCoolTime <= 1.6f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-17, -4, 0), 0.5f);
        }
        else if (startCoolTime <= 2.4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -13, 0), 0.5f);
        }
        else if (startCoolTime <= 3.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(17, -1, 0), 0.5f);
        }
        else
        {
            startCoolTime = 0;
        }
    }

}
