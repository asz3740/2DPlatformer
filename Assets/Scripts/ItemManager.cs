using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Weapon weapon;
    public Player player;

    private static ItemManager _instance;

    public static ItemManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(ItemManager)) as ItemManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ItemCoin()
    {
        int ran = Random.Range(10, 61);
        GameManager.Instance.totalCoin += ran;
    }

    public void ItemPotion()
    {
        print("들들들");
        player.HP += 15;
        if (player.HP > player.startHP)
        {
            player.HP = player.startHP;
        }
    }

    public void ItemBush()
    {
        int ran = Random.Range(0, 3);
        if (ran == 0)
        {
            GameManager.Instance.totalCoin += 150;
        }
        else
        {
            player.HP -= 5;
        }
    }

    public void ItemArmor()
    {
        print("startHP1" + player.startHP);
        print("HP1" + player.HP);
        player.startHP += 25;
        player.HP += 25;
        print("startHP2" + player.startHP);
        print("HP2" + player.HP);
    }

    public void ItemGlove()
    {
        print("1" + player.RangedAtkCool);
        if (player.RangedAtkCool <= 2f)
        {
            print("안됨");
            return;
        }
          

        player.RangedAtkCool -= 1f;
        print("2" + player.RangedAtkCool);

    }

    public void ItemAxe()
    {
        weapon.Upgrade();
    }

    public void ItemApple()
    {
        player.StartCoroutine("HPHeal");
    }
}
