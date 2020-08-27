using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    static StoreManager instance;
    public static StoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StoreManager>();
            }

            return instance;
        }

    }

    public GameObject storeCards;
    public Text sellpriceText;
    public int[] sellPrice;

    public int uintMax;
    public bool isLock = false;
    public GameObject lockOpenImage;
    public GameObject lockCloseImage;

    public int currentLv;
    public int currentMaxExp;
    public int currentExp;

    public int[] maxExps;

    





    public int card1Percentage;
    public int card2Percentage;
    public int card3Percentage;
    public int card4Percentage;

    public Text card1PercentageText;
    public Text card2PercentageText;
    public Text card3PercentageText;
    public Text card4PercentageText;


   
    public int refreshCost;
    public int xp_Up_Cost;



    public ThisCard[] cards;
    public GameObject[] storeDropZones;

    // Start is called before the first frame update
    void Awake()
    {
        uintMax = 2;
        xp_Up_Cost = 4;
        currentExp = 0;
         currentLv = 1;
       
        refreshCost = 2;
        cards = FindObjectsOfType<ThisCard>();

        PercentageUpdate();
    }

    #region 잠금기능
    public void LockBtn()
    {
        if (isLock)
        {
            isLock = false;
            lockOpenImage.SetActive(true);
            lockCloseImage.SetActive(false);
        }
        else
        {
            isLock = true;
            lockOpenImage.SetActive(false);
            lockCloseImage.SetActive(true);
            
        }
    }
    #endregion


    public void XP_BTN()
    {

        if(currentLv < 9)
        {
            if (StageManager.Instance.currentGold >= 4)
            {
                Xp_Up(4);
                StageManager.Instance.currentGold -= 4;
            }
        }


    }

    public void Xp_Up(int num)
    {

        if (currentLv < 9)
        {
            currentExp += num;
            if (currentExp >= maxExps[currentLv])
            {

                if (currentLv < 9)
                {
                    currentLv++;
                }

                currentExp = 0;
            }

            PercentageUpdate();
        }
            
    
    }

    public void Refresh()
    {
        if (StageManager.Instance.currentGold >= refreshCost)
        {
            StageManager.Instance.currentGold -= refreshCost;
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].NewCard();
                cards[i].gameObject.SetActive(true);
            }

            if (isLock)
            {
                LockBtn();
            }
        }

    }

    public void Refresh_wave()
    {
       
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].NewCard();
                cards[i].gameObject.SetActive(true);
            }

    }


    //레벨당 확률계산 유닛 맥스도 포함시킴
    public void PercentageUpdate()
    {
        switch (currentLv)
        {
            case 1 :
                card1Percentage = 100;
                card2Percentage = 0;
                card3Percentage = 0;
                card4Percentage = 0;
                uintMax = 1;
                break;
            case 2:
                card1Percentage = 80;
                card2Percentage = 20;
                card3Percentage = 0;
                card4Percentage = 0;
                uintMax = 2;
                break;
            case 3:
                card1Percentage = 70;
                card2Percentage = 25;
                card3Percentage = 5;
                card4Percentage = 0;
                uintMax = 3;
                break;
            case 4:
                card1Percentage = 55;
                card2Percentage = 35;
                card3Percentage = 10;
                card4Percentage = 0;
                uintMax = 4;
                break;
            case 5:
                card1Percentage = 49;
                card2Percentage = 35;
                card3Percentage = 15;
                card4Percentage = 1;
                uintMax = 5;
                break;
            case 6:
                card1Percentage = 40;
                card2Percentage = 35;
                card3Percentage = 20;
                card4Percentage = 5;
                uintMax = 6;
                break;
            case 7:
                card1Percentage = 30;
                card2Percentage = 35;
                card3Percentage = 25;
                card4Percentage = 10;
                uintMax = 7;
                break;
            case 8:
                card1Percentage = 25;
                card2Percentage = 30;
                card3Percentage = 30;
                card4Percentage = 15;
                uintMax = 8;
                break;
            case 9:
                card1Percentage = 20;
                card2Percentage = 25;
                card3Percentage = 35;
                card4Percentage = 20;
                uintMax = 9;
                break;

            default:
                break;
        }

        card1PercentageText.text = "" + card1Percentage + "%";
        card2PercentageText.text = "" + card2Percentage + "%";
        card3PercentageText.text = "" + card3Percentage + "%";
        card4PercentageText.text = "" + card4Percentage + "%";

    }


}
