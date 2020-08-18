using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(transform.right * 10 * Time.deltaTime);
        Destroy(gameObject,15);
    }
  
}
