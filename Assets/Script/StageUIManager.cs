using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoBehaviour
{

    static StageUIManager instance;
    public static StageUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageUIManager>();
            }

            return instance;
        }

    }



    public Text debugText;
    public Text gold_text;

    public Text ttime;
    public Text waveText;
    public Text stopWaveText;
    public Text gameOverWaveText;

    public Text passwaveText;
    public Text refreshCostText;
    public Text xp_Up_CostText;

    public Slider expSlider;
    public Text expSliderText;

    public Text currentLvText;

    public GameObject currentEnemyNumWindow;
    public GameObject currentEnemyImage_monster;
    public GameObject currentEnemyImage_time;
    public Text currentEnemyNumText;

    public Slider leaderHpSlider;
    public Text leaderHpSliderText;

    public GameObject gameOverWindow;
    public Text gameOverDiaText;
    public Text gameOverText;

    public GameObject[] gameInfo;
    public GameObject[] synergyInfo;
    public GameObject synergyInfoWindow;

    public GameObject[] interestImages;

    public Text currentUnitText;


    public GameObject NextBtn;


   

    private void Update()
    {
        currentUnitText.text = "" + StageManager.Instance.currentUintNum + " / " + StoreManager.Instance.uintMax;
       
        gold_text.text =  StageManager.Instance.currentGold + " Gold";
        waveText.text = "Wave  " + StageManager.Instance.currentWave + "  /  30";
        passwaveText.text = "Wave " + StageManager.Instance.currentWave;
        stopWaveText.text = "Wave " + StageManager.Instance.currentWave;
        //gameOverWaveText.text = "Wave " + StageManager.Instance.currentWave;

        refreshCostText.text = "" + StoreManager.Instance.refreshCost + " Gold";
        xp_Up_CostText.text = "" + StoreManager.Instance.xp_Up_Cost + " Gold";
        expSliderText.text = "" + StoreManager.Instance.currentExp + " / " + StoreManager.Instance.maxExps[StoreManager.Instance.currentLv];
        currentLvText.text = "Lv " + StoreManager.Instance.currentLv;

       

        expSlider.maxValue = StoreManager.Instance.maxExps[StoreManager.Instance.currentLv];
        expSlider.value = StoreManager.Instance.currentExp;

        leaderHpSlider.maxValue = StageManager.Instance.LeaderMaxHp;
        leaderHpSlider.value = StageManager.Instance.currentLeaderHp;
        leaderHpSliderText.text = "" + StageManager.Instance.currentLeaderHp + "/" + StageManager.Instance.LeaderMaxHp;

        if (StoreManager.Instance.currentLv >= 9 && expSlider.gameObject.activeSelf)
        {
            expSlider.gameObject.SetActive(false);
        }

        //진행중일때
        if (StageManager.Instance.isWaving)
        {
            ttime.text = "남은 몬스터 : " + StageManager.Instance.currentEnemyNum;
            currentEnemyImage_monster.SetActive(true);
            currentEnemyImage_time.SetActive(false);
            currentEnemyNumText.text = "" + StageManager.Instance.currentEnemyNum;
            gameInfo[0].SetActive(false);
            gameInfo[1].SetActive(true);

            NextBtn.SetActive(false);
        }
        else
        {

            ttime.text = "Time : " + (int)StageManager.Instance.ttime;
            currentEnemyImage_monster.SetActive(false);
            currentEnemyImage_time.SetActive(true);
            currentEnemyNumText.text = "" + StageManager.Instance.ttime.ToString("N1") ;
            gameInfo[0].SetActive(true);
            gameInfo[1].SetActive(false);
            NextBtn.SetActive(true);
        }

        //이자이미지
        switch (StageManager.Instance.currentInterest)
        {
            case 0:
                for (int i = 0; i < interestImages.Length; i++)
                {
                    interestImages[i].SetActive(false);
                }
                break;
            case 1:
                for (int i = 0; i < interestImages.Length; i++)
                {
                    interestImages[i].SetActive(false);
                    interestImages[0].SetActive(true);
                }
                break;
            case 2:
                for (int i = 0; i < interestImages.Length; i++)
                {
                    interestImages[i].SetActive(false);
                    interestImages[0].SetActive(true);
                    interestImages[1].SetActive(true);
                }
                break;
            case 3:
                for (int i = 0; i < interestImages.Length; i++)
                {
                    interestImages[i].SetActive(false);
                    interestImages[0].SetActive(true);
                    interestImages[1].SetActive(true);
                    interestImages[2].SetActive(true);
                }
                break;
            case 4:
                for (int i = 0; i < interestImages.Length; i++)
                {
                    interestImages[i].SetActive(false);
                    interestImages[0].SetActive(true);
                    interestImages[1].SetActive(true);
                    interestImages[2].SetActive(true);
                    interestImages[3].SetActive(true);
                }
                break;
        }

    }

    public void SynergyInfoOpen(int num)
    {
        synergyInfoWindow.SetActive(true);
        for (int i = 0; i < synergyInfo.Length; i++)
        {
            synergyInfo[i].SetActive(false);
        }

        synergyInfo[num].SetActive(true);
    }

    public void CloseSynergyInfo()
    {
        synergyInfoWindow.SetActive(false);
        for (int i = 0; i < synergyInfo.Length; i++)
        {
            synergyInfo[i].SetActive(false);
        }
    }

    public void NextBtnClick()
    {

        StageManager.Instance.ttime = 0.1f;
        NextBtn.SetActive(false);


    }



}
