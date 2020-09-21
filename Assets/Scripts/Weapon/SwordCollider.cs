using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : Weapon
{
    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == targetTag)
        {
            print("1111");
            GetComponent<Collider2D>().enabled = false;
        }
    }
}