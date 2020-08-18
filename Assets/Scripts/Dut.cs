//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Dut : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void OnTriggerEnter2D(Collider2D col)
//    {
//        if (col.transform.tag == player)
//        {
//            rb2D.gravityScale = 1f;
//            rb2D.AddForce(Vector3.down * 3f * Time.deltaTime, ForceMode2D.Impulse);
//        }
//    }

//    void OnCollisionEnter2D(Collision2D col)
//    {
//        if (col.transform.tag == "Ground")
//        {
//            StartCoroutine(backOrigin());
//        }
    
//    IEnumerator backOrigin()
//    {
//        rb2D.gravityScale = 1f;

//        float sqrRemaingDistance = (transform.position - originPos).sqrMagnitude;

//        while (sqrRemaingDistance > float.Epsilon + 0.1f)
//        {
//            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, originPos, rBackSpeed * Time.deltaTime);
//            rb2D.MovePosition(newPosition);
//            sqrRemaingDistance = (transform.position - originPos).sqrMagnitude;

//            yield return null;
//        }
//        rb2D.gravityScale = 0f;
//        rb2D.velocity = Vector3.zero;
//    }
//}
