using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] stages;

    [SerializeField]
    private GameObject[] item; 

    public float spawnCool = 5f;
    public float spawnCoolTime;

    public int deadMonsterCount;

    public int totalCoin = 0;
    public int stageIndex = 0;
    public Player player;
    //public Boss boss;
  

    public Image PlayerHPbarI;
    public Text playerHPT;

    //public Image BossHPbar;
    //public Text BossHPtext;

    [SerializeField]
    private Text stageNameT;

    [SerializeField]
    private Text coinSumT;
    //public Text Attack;

    //public GameObject gameoverText;
    //public GameObject BossHP;
    //bool isGameOver = false;

    //public GameObject menuSet;


    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

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

    private float stageNameFalseCoolTime;
    private float stageNameFalseCool = 5f;

    void Start()
    {
        stageNameT.text = "침략의 시작";
    }

    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            print(stageIndex);
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);

            if (stageIndex == 3)
                player.isSwimming = true;
            else
                player.isSwimming = false;

            if (stageIndex == 4)
            {
                //BossHP.SetActive(true);
            }
            PlayerReposition();
            deadMonsterCount = 0;
            //player.MonsterCount = 0;
        }
        else
        {
            Time.timeScale = 0;

            Debug.Log("게임 클리어!");
        }




        //totalCoin += stageCoin;
        //stageCoin = 0;
    }

    //void Update()
    //{
    //    //if (Input.GetButtonDown("Cancel"))
        //{
        //    if (menuSet.activeSelf)
        //    {
        //        menuSet.SetActive(false);
        //        Time.timeScale = 1;
        //    }

        //    else
        //    {
        //        menuSet.SetActive(true);
        //        Time.timeScale = 0;
        //    }
        //}



        //curSpawnDelay += Time.deltaTime;


        //Coin.text = (totalCoin + stageCoin).ToString();
        //HPbar();
        //if (isGameOver)
        //{
        //    if (Input.GetKeyDown(KeyCode.R))
        //    {
        //        SceneManager.LoadScene("Main_Lobby_Map");
        //    }
        //}

    //}

    void Update()
    {
        stageNameFalseCoolTime += Time.deltaTime;

        if (stageNameFalseCoolTime > stageNameFalseCool)
        {
            stageNameT.gameObject.SetActive(false);
        }

        spawnCoolTime += Time.deltaTime;

        HPbarUI();
        CoinUI();

    }
    private void PlayerReposition()
    {
        player.transform.position = new Vector3(-15, -9, 0);
        //player.VelocityZero();
    }

    private void HPbarUI()
    {
        float HP = player.HP;
        float MAXHP = player.startHP;
        if (HP < 0)
        {
            MAXHP = 0;
        }
        PlayerHPbarI.fillAmount = HP / MAXHP;
        playerHPT.text = string.Format("HP {0}/{1}", HP, MAXHP);

        //float BossHP = boss.HP;
        //BossHPbar.fillAmount = BossHP / 500f;
        //BossHPtext.text = string.Format("HP {0}/500", BossHP);

        //float ATT = player.ATT;
        //Attack.text = ATT.ToString();

    }

    private void CoinUI()
    {
        coinSumT.text = string.Format("{0}", totalCoin);
    }


    public void CoinSum(int coin)
    {
        totalCoin += coin;
    }

    public void RandomItem(Transform enemyTransfrom)
    {
        int ranItem = Random.Range(0, 3);
        Instantiate(item[ranItem], enemyTransfrom.position, enemyTransfrom.rotation);
    }



    public void EndGame()
    {
        //gameoverText.SetActive(true);
        //isGameOver = true;
        //totalCoin += stageCoin;
        //Gamesave();
        Debug.Log("끝");
    }

    //public void GameExit()
    //{
    //    Application.Quit();
    //}


   








    


}

