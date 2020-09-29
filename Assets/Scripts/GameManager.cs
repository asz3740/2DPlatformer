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

    public GameObject BossHP;

    [SerializeField]
    private GameObject won;

    [SerializeField]
    private Text wonText;

    [SerializeField]
    private GameObject gameOver;

    private float gamePlayTime;

    [SerializeField]
    private GameObject shopExplanation;

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
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public GameObject wall;

    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);

            if(stageIndex == 3)
            {
                shopExplanation.SetActive(true);
            }
            if (stageIndex == 4)
            {
                shopExplanation.SetActive(false);
                cinemachine.SetActive(false);
                boss = GameObject.Find("Boss").GetComponent<Boss>();
                BossHP.SetActive(true);
            }
            PlayerReposition();
        }
    }

    void Update()
    {
        gamePlayTime += Time.deltaTime;
        gameTimer.text = "" + Mathf.Round(gamePlayTime);



        spawnCoolTime += Time.deltaTime;

        HPbarUI();
        if (stageIndex == 4)
            BossHPbarUI();
        CoinUI();

    }
    private void PlayerReposition()
    {
        player.transform.position = new Vector3(-15, -9, 0);
    }

    private void HPbarUI()
    {

        float playerHP = player.HP;
        float playerMaxHP = player.startHP;
        if (playerHP < 0)
        {
            playerMaxHP = 0;
        }
        PlayerHPbarI.fillAmount = playerHP / playerMaxHP;
        playerHPT.text = string.Format("HP {0}/{1}", playerHP, playerMaxHP);

     
    }

    private void BossHPbarUI()
    {
        float bossHP = boss.HP;
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
        gameOver.SetActive(true);
    }

    public void SuccessGame()
    {
        player.gameObject.SetActive(false);
        won.SetActive(true);
        wonText.text = Mathf.Round(gamePlayTime) + "초";

    }

}

