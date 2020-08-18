using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireSpirit : MonoBehaviour
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

            if (Input.GetKeyDown(KeyCode.G) && check)
            {
                check = false;
                talkPanel.SetActive(true);
                portraitImg.sprite = portraitArr[0];
                talkText.text = "ㅎ... 덕분에 정신은 차렸는데... 역시 힘드네...";
                StartCoroutine(WaitForIt());
            }
        }


    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(2);
        check = true;
        portraitImg.sprite = portraitArr[1];
        talkText.text = "이렇게 도와줬으니 나도 힘을 좀 써야지";
        yield return new WaitForSeconds(2);

        portraitImg.sprite = portraitArr[2];
        talkText.text = "밖으로 보내줄게 조심하고 다음에 보자";
        Invoke("SceneChange", 2);
    }

    private void SceneChange()
    {
        SceneManager.LoadScene("Main_Lobby_Map");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        talkPanel.SetActive(false);
    }

}