using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    static InfoWindow instance;
    public static InfoWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InfoWindow>();
            }

            return instance;
        }

    }

    public GameObject currentCharacter;

    public GameObject window;

    public Text Lv;


    public Text id;

    public Text name_;

    public Text rating;

    public Text[] synergy_texts;

    public Image[] synergy_images;

    public string synergy4;

    public Text ATK;

    public Text ATKspeed;

    public Text fatal;

    public Text ATKrange;

    public Text sp;

    public Text magicATK;

    public Text skill_info;

    public Image thisImage;

    public Text myid;

    public Text atkCool;

    public Text sellPriceText;

    public void OpenWindow(float range ,Transform pos)
    {

        if (currentCharacter != null)
        {
            CloseWindowBtn();
        }

        window.SetActive(true);

        

        if (pos.gameObject.GetComponent<Character>().cha_prefab_.tag == "Stone")
        {
            pos.gameObject.GetComponent<Character>().ShowStoneRange();
        }
        else
        {
            RangeCircle.Instance.GetComponent<LineRenderer>().enabled = true;
            RangeCircle.Instance.CreatePoints(range, pos);
        }
            

       
    }


    public void CloseWindow()
    {
        window.SetActive(false);
        //  RangeCircle.Instance.GetComponent<LineRenderer>().enabled = false;

       

    }

    public void CloseWindowBtn()
    {
        window.SetActive(false);
        RangeCircle.Instance.GetComponent<LineRenderer>().enabled = false;

        if (currentCharacter != null)
        {
            currentCharacter.GetComponent<Character>().CloseStoneRange();
        }
        

    }

    public void SellCharacter()
    {
       
        StageManager.Instance.currentGold += (currentCharacter.GetComponent<Character>().Lv  * currentCharacter.GetComponent<Character>().rating * 1) ;
        currentCharacter.gameObject.transform.parent.GetComponent<DropZone>().currentObj = null;
        currentCharacter.transform.SetParent(null);
        RangeCircle.Instance.transform.SetParent(null);
        Destroy(currentCharacter);
        CloseWindowBtn();

        SynergyManager.Instance.FindSynergy();
        StageManager.Instance.CurrentUnitCheck();


    }





}
