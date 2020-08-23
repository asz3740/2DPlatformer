using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCTalk : MonoBehaviour
{
    public GameObject explanationT;
    public Text talkText;
    public GameObject talkPanel;
    public GameObject explanationPanel;
    public Image portraitImg;
    public Sprite temple;
    public Player player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkPanel.SetActive(true);
            portraitImg.sprite = temple;
            talkText.text = "어서 오거라!";

            StartCoroutine(WaitForIt());
            player.speed = 0;
        }


    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2);
        talkText.text = "시노의 악에 기운이 더욱 강해지고 있다..";
        yield return new WaitForSeconds(4);
        talkText.text = "사이야.. 시노를 막을 수 있는 기회는 지금뿐이다..";
        yield return new WaitForSeconds(4);
        talkText.text = "모두 내 잘못이구나.. 미안하다..";
        yield return new WaitForSeconds(4);
        talkText.text = "부탁하마..";
        yield return new WaitForSeconds(4);

        talkPanel.SetActive(false);
        explanationT.SetActive(false);
        explanationPanel.SetActive(true);
    }
}
