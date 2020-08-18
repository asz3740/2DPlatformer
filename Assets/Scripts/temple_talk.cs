using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temple_talk : MonoBehaviour
{
    public Text talkText;
    public GameObject talkPanel;
    public GameObject player;
    public bool check = true;
    public Image portraitImg;
    public Sprite temple;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.G) && check)
            {
                check = false;
                talkPanel.SetActive(true);
                portraitImg.sprite = temple;
                talkText.text = "어떤 쿠키인지는 잘 모르겠지만 온화한 인상을 가지고 있다.";
                StartCoroutine(WaitForIt());

            }
        }


    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2);
        check = true;
        talkText.text = "잘 모르겠지만 알수 없는 기류가 흐른다.";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        talkPanel.SetActive(false);
    }

}
