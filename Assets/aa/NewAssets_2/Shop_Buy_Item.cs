using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Buy_Item : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject Item_area;
    public GameObject Player;
    public GameObject Item;
    public Text Ex;
    public Text Ex1;
    public int count;
    SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private PlayerMove player;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag=="Player")
    //    {
    //        Ex.gameObject.SetActive(true);
    //        if (Input.GetKey(KeyCode.G))
    //        {
    //            BuyItem(Item);
    //        }
    //    }          

    //}

    void OnTriggerExit2D(Collider2D collision)
    {
        Ex.gameObject.SetActive(false);
        Ex1.gameObject.SetActive(false);
    }

    //void BuyItem(GameObject item)
    //{
    //    if (item.gameObject.tag == "bubblegun" && gameManager.totalCoin >= 300 && count <=1)
    //    {
    //        gameManager.totalCoin -= 250;
    //        player.ATT += 3;
    //        count++;
    //        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    //    }
    //    else if (item.gameObject.tag == "hp" && gameManager.totalCoin >= 150 && count <= 1)
    //    {
    //        gameManager.totalCoin -= 150;
    //        count++;
    //        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    //        if (player.MaxHP <= player.HP + 20)
    //        {
    //            player.HP = player.MaxHP;
    //        }
    //        else
    //            player.HP += 10;
    //    }
    //    else
    //        Ex1.gameObject.SetActive(true);
    //}
}
