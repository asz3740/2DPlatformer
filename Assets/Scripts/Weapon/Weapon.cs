using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public void Upgrade()
    {
        print("Damaged"+ damage);
        if(gameObject.tag =="PlayerWeapon")
        { 
            damage += 10;
        }
        print("Damaged2" + damage);
    }
}
