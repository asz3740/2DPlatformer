using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManger : MonoBehaviour
{
    
    public Text talkText;
    public GameObject talkPanel;
    public GameObject player;
    public bool check = true;
    public Image portraitImg;
    public Sprite[] portraitArr;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.D) && check)
            {
                check = false;
                talkPanel.SetActive(true);
                portraitImg.sprite = portraitArr[0];
                talkText.text = "이 앞은 위험해 이곳을 나가기 위해서는 감당해야하지";
                StartCoroutine(WaitForIt());

            }
        }


    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2);
        check = true;
        portraitImg.sprite = portraitArr[1];
        talkText.text = "일단 기본 조작은 W A S D 대쉬는 F 공격은 Left Click" + "스킬은 Right Click 마지막으로 점프는 Space야";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        talkPanel.SetActive(false);
    }

}


