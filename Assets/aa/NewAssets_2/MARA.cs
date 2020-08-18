using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MARA : MonoBehaviour
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
                talkText.text = "여기는 훈련소야 원한다면 너희도 여기서 강해질수 있지";
                StartCoroutine(WaitForIt());

            }
        }


    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2);
        check = true;
        talkText.text = "하지만 내 방식은 스파르타 이니 각오는 해두라구";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        talkPanel.SetActive(false);
    }

}
