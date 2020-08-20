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

    [SerializeField]
    private float spawnCool = 5f;
    private float spawnCoolTime;

    public int totalCoin = 0;

    [SerializeField]
    private int stageIndex = 0;

    private Player player;
    private Boss boss;

    [SerializeField]
    private Image PlayerHPbarI;
    [SerializeField]
    private Text playerHPT;

    [SerializeField]
    private Image bossHPbarI;
    [SerializeField]
    private Text bossHPT;

    [SerializeField]
    private Text stageNameT;

    [SerializeField]
    private Text coinSumT;

    [SerializeField]
    private Text gameTimer;

    [SerializeField]
    public GameObject cinemachine;
    //public Text Attack;

    //public GameObject gameoverText;
    public GameObject BossHP;
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
        player = GameObject.Find("Player").GetComponent<Player>();
        stageNameT.text = "침략의 시작";

    }

    public GameObject wall;

    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            print(stageIndex);
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);

            if (stageIndex == 3)
            {
                wall.SetActive(false);
                player.isSwimming = true;
            }
            else
                player.isSwimming = false;

            if (stageIndex == 4)
            {
                cinemachine.SetActive(false);
                boss = GameObject.Find("Boss").GetComponent<Boss>();
                BossHP.SetActive(true);
            }
            PlayerReposition();
        }
        else
        {
            Time.timeScale = 0;

            Debug.Log("게임 클리어!");
        }
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
        gameTimer.text = "" + Mathf.Round(stageNameFalseCoolTime);

        if (stageNameFalseCoolTime > stageNameFalseCool)
        {
            stageNameT.gameObject.SetActive(false);
        }

        spawnCoolTime += Time.deltaTime;

        HPbarUI();
        if (stageIndex == 4)
            BossHPbarUI();
        CoinUI();

    }
    private void PlayerReposition()
    {
        player.transform.position = new Vector3(-15, -9, 0);
        //player.VelocityZero();
    }

    private void HPbarUI()
    {

        float playerHP = player.HP;
        print("playerHP" + playerHP);
        float playerMaxHP = player.startHP;
        if (playerHP < 0)
        {
            playerMaxHP = 0;
        }
        PlayerHPbarI.fillAmount = playerHP / playerMaxHP;
        playerHPT.text = string.Format("HP {0}/{1}", playerHP, playerMaxHP);

      

        //float ATT = player.ATT;
        //Attack.text = ATT.ToString();

    }

    private void BossHPbarUI()
    {
        float bossHP = boss.HP;
        print(" boss.HP" + boss.HP);
        print("bossHP" + bossHP);
        //float bossMaxHP = 700f;
        bossHPbarI.fillAmount = bossHP / 1000;
        bossHPT.text = string.Format("HP {0}/1000", bossHP, 1000f);
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

    public void SuccessGame()
    {
        print("승리");
    }

    //public void GameExit()
    //{
    //    Application.Quit();
    //}














}

