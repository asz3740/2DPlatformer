using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Player player;
    //public GameObject Item;
    public Text ex;
    public Text won;
    public Text itemEx;

    private bool isClick;

    void Start()
    {
        itemEx.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(isClick)
                BuyItem();
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ex.gameObject.SetActive(true);
            won.gameObject.SetActive(true);
            isClick = true;       
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isClick = false;
        ex.gameObject.SetActive(false);
        won.gameObject.SetActive(false);
    }

    public GameObject potion;
    public int count;
    void BuyItem()
    {
        if (gameObject.tag == "Armor" && GameManager.Instance.totalCoin >= 200)
        {
            ItemManager.Instance.ItemArmor();
            GameManager.Instance.totalCoin -= 200;          
        }
        else if (gameObject.tag == "Glove" && GameManager.Instance.totalCoin >= 300 && count < 2)
        {
            count++;
            ItemManager.Instance.ItemGlove();
            GameManager.Instance.totalCoin -= 300;
        }
        else if (gameObject.tag == "Potion" && GameManager.Instance.totalCoin >= 130)
        {
            Instantiate(potion,transform.position, transform.rotation);
            GameManager.Instance.totalCoin -= 130;
        }
        else if (gameObject.tag == "Axe" && GameManager.Instance.totalCoin >= 400)
        {
            ItemManager.Instance.ItemAxe();
            GameManager.Instance.totalCoin -= 400;
        }
        else if (gameObject.tag == "Apple" && GameManager.Instance.totalCoin >= 350)
        {
            ItemManager.Instance.ItemApple();
            GameManager.Instance.totalCoin -= 350;
        }

    }
}
