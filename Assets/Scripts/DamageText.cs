﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
     public float moveSpeed;
     public float alphaSpeed;
     public float destroyTime;
     public Text text;
     Color alpha;
     public int damage;

    void Start()
    {

        //text = GetComponent<Text>();
        alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {

        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        print("1"); 
        Destroy(gameObject);
    }
}
