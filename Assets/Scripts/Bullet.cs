using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward * 0.1f);
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // PlayerMove PlayMove = collision.GetComponent<PlayerMove>();

            // if (PlayMove != null)
            //{
            //collision.GetComponent<PlayerMove>().TakeDamage(5);
        
            //PlayMove.OnDamaged(collision.transform.position);
            //}
        }
    }
}

